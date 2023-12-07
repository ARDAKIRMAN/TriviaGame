using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class LeaderboardManager : Singleton<LeaderboardManager>
{
    public GameObject content;
    int pageLoadOffset = 1;
	public List<LeaderboardBase> loadedPlayerData;
	public List<LeaderboardData> loadedLeaderboardPages;
    public LeaderboardScroll scroll;
    bool loadingNewPage;
    public bool isLocalPlayerShown;
    private void Update()
	{
        CheckForUpdate();
    }
    public void ClearData()
    {
        loadedPlayerData.Clear();
        loadedLeaderboardPages.Clear();
        scroll.scroller.ClearAll();
    }
    void CheckForUpdate()
    {
        if (content.activeSelf && !loadingNewPage && scroll.scroller.NumberOfCells - scroll.scroller.EndCellViewIndex <= pageLoadOffset)
        {
            loadingNewPage = true;
            LoadPage();
        }
    }
	public async void LoadPage(Action _action = null)
    {
        loadingNewPage = true;
        var lastLoadedPage = loadedLeaderboardPages.Count > 0 ? loadedLeaderboardPages[loadedLeaderboardPages.Count - 1] : null;

        if (lastLoadedPage != null && lastLoadedPage.is_last == "true")
        {
            loadingNewPage = false;
            return;
        }

        int page = lastLoadedPage != null ? Convert.ToInt32(lastLoadedPage.page) + 1 : 0;

        Debug.Log("Loading " + page);
        Task<LeaderboardData> task = Reader.Instance.GetLeaderboardAsync(page);

        var _leaderboardPage = await task;

        if (_leaderboardPage != null)
        {
            Debug.Log("Page received!");
            Debug.Log(_leaderboardPage.page);
            loadedPlayerData.AddRange(_leaderboardPage.data);
            loadedLeaderboardPages.Add(_leaderboardPage);
        }
        else
        {
            Debug.LogError("Failed to receive leaderboard page!");
        }
		if (Convert.ToInt32(loadedPlayerData.Last().score) <= GetScore() || loadedLeaderboardPages.Last().is_last == "true")
		{
            int localRank = -1;
            var _op = loadedPlayerData.Where(x => Convert.ToInt32(x.score) <= GetScore());

            if (_op.Any())
                localRank = Convert.ToInt32( Convert.ToInt32(_op.First().rank) -1);

            if (localRank != -1 && !isLocalPlayerShown)
            {
                LeaderboardBase localData = new LeaderboardBase();
                localData.nickname = "LocalPlayer";
                localData.rank = localRank.ToString();
                localData.score = GetScore().ToString();
                localData.isLocalPlayer = true;
                loadedPlayerData.Insert(localRank, localData);
                isLocalPlayerShown = true;
            }
			for (int i = 0; i < loadedPlayerData.Count; i++)
			{
                loadedPlayerData[i].rank = (i + 1).ToString();
            }
        }
        loadingNewPage = false;
        scroll.UpdateLeaderboard(loadedPlayerData);
        _action?.Invoke();
    }
   
    int GetScore()
	{
        return PlayerPrefs.GetInt("Score");
	}
}
[System.Serializable]
public class LeaderboardBase
{
    public string rank;
    public string nickname;
    public string score;
    public bool isLocalPlayer;
}
[System.Serializable]
public class LeaderboardData
{
    public string page;
    public string is_last;
    public List<LeaderboardBase> data;
}