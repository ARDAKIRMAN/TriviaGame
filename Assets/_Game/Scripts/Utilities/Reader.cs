using Newtonsoft.Json;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public class Reader : Singleton<Reader>
{
    public bool questionsLoaded;
    public List<LeaderboardData> loadedPages = new List<LeaderboardData>();
	public async Task<List<Question>> GetQuestionsAsync()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("https://mocki.io/v1/e63d59db-7769-45a9-b76f-28780201d493"))
        {
            var asyncOperation = webRequest.SendWebRequest();

            while (!asyncOperation.isDone)
            {
                await Task.Yield();
            }

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + webRequest.error);
                return null;
            }
            else
            {
                string json = webRequest.downloadHandler.text.Replace("\n", "");
                QuestionsData questionData = JsonConvert.DeserializeObject<QuestionsData>(json);
				foreach (var q in questionData.questions)
				{
                    if (!QuestionManager.Instance.questions.Where(x=> x.question.question == q.question).Any())
                        QuestionManager.Instance.questions.Add(new Question() { question = q});
				}
                return QuestionManager.Instance.questions;
            }
        }
    }
    public async void LoadQuestions()
    {
        Task<List<Question>> task = GetQuestionsAsync();

        QuestionManager.Instance.questions = await task;

        if (QuestionManager.Instance.questions != null)
        {
            Debug.Log("Questions received!");
            questionsLoaded = true;
        }
        else
        {
            Debug.LogError("Failed to receive questions!");
        }
    }

    public async Task<LeaderboardData> GetLeaderboardAsync(int page)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get("https://magegamessite.web.app/case1/leaderboard_page_" + page + ".json"))
        {
            var asyncOperation = webRequest.SendWebRequest();

            while (!asyncOperation.isDone)
            {
                await Task.Yield();
            }

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + webRequest.error);
                return null;
            }
            else
            {
                string json = webRequest.downloadHandler.text;
                Debug.Log(json);
                LeaderboardData leaderboardData = JsonUtility.FromJson<LeaderboardData>(json);
                return leaderboardData;
            }
        }
    }
}

//prefe kayetme dat olarak kaydet
//obs et
//
