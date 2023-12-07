using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionManager : Singleton<QuestionManager>
{
	public List<Question> questions = new List<Question>();
	public bool gettinQuestions;
	public Question GetQuestion()
	{
		foreach (var q in questions)
		{
			if (!q.shown) { Debug.Log("question"); q.shown = true; return q; }
		}
		return null;
	}
	public bool CheckQuestions()
	{
		if (Reader.Instance.questionsLoaded)
		{
			gettinQuestions = false;
			return true;
		}
		else
		{
			if (!gettinQuestions)
			{
				gettinQuestions = true;
				Reader.Instance.LoadQuestions();
			}
			return false;
		}
	}
}
[System.Serializable]
public class QuestionsData
{
	public QuestionBase[] questions;
}
[System.Serializable]
public class QuestionBase
{
	public string category;
	public string question;
	public string[] options;
	public string answer;
}
[System.Serializable]
public class Question
{
	public QuestionBase question;
	public bool shown = false;
}