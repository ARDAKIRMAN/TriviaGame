using EnhancedUI;
using EnhancedUI.EnhancedScroller;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardScroll : MonoBehaviour , IEnhancedScrollerDelegate
{
    public EnhancedScroller scroller;
    private SmallList<LeaderboardElementData> _data;
    public EnhancedScrollerCellView cellViewPrefab;

    void Start()
    {
        scroller.Delegate = this;
    }
	public void UpdateLeaderboard(List<LeaderboardBase> dataList)
	{
        _data = new SmallList<LeaderboardElementData>();
        for (var i = 0; i < dataList.Count; i++)
            _data.Add(new LeaderboardElementData() { nickname = dataList[i].nickname, rank = dataList[i].rank, score = dataList[i].score , isLocalPlayer = dataList[i].isLocalPlayer });

        scroller.ReloadData();
        Debug.Log("Leaderboard Update");
    }
    public EnhancedScrollerCellView GetCellView(EnhancedScroller scroller, int dataIndex, int cellIndex)
	{
        CellView cellView = scroller.GetCellView(cellViewPrefab) as CellView;
        cellView.name = "Cell " + dataIndex.ToString();
        cellView.SetData(_data[dataIndex]);
        return cellView;
    }

	public float GetCellViewSize(EnhancedScroller scroller, int dataIndex)
	{
        return (100f);
    }

	public int GetNumberOfCells(EnhancedScroller scroller)
	{
        if (_data != null)
            return _data.Count;
        else
            return 0;
    }


}
