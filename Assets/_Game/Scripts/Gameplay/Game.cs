using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game: MonoBehaviour
{
	public QuestionTimer questionTimer;
	public PowerUps powerUps;

	public int score;
	public int consecutiveCorrectAnswers;

	public bool hasShield;

	public Action onGameOver;
	public Action onCorrectAnswer;
	public Action onWrongAnswer;
	public Action onTimeOut;
	public Action onPrepareGame;
	public Action onNextQuestion;
	public void Awake()
	{
		onCorrectAnswer += OnCorrectAnswer;
		onWrongAnswer += OnWrongAnswer;
		onTimeOut += OnTimeOut;
	}
	private void Start()
	{
		StartCoroutine(LateStart());
	}
	IEnumerator LateStart()
	{
		yield return new WaitForEndOfFrame();
		Fader.Instance.FadeIn();
		onPrepareGame?.Invoke();
	}
	public void ActivateShield()
	{
		hasShield = true;
	}	
	public void DeactivateShield()
	{
		hasShield = false;
	}
	private void OnCorrectAnswer()
	{
		consecutiveCorrectAnswers = Mathf.Clamp(consecutiveCorrectAnswers++, 0, 3);
		score += PersistentManagers.Instance.settings.correctReward;
		powerUps.AddArmor();
		FXManager.Instance.ShowFX(EFxType.WinEffect);
	}
	private void OnWrongAnswer()
	{
		score -= PersistentManagers.Instance.settings.wrongPenalty;
		StartCoroutine(WrongAnswerRoutine());
	}
	IEnumerator WrongAnswerRoutine()
	{
		powerUps.RemoveArmors();
		FXManager.Instance.ShowFX(EFxType.LoseEffect);

		if (hasShield)
			powerUps.DisableShied();

		yield return new WaitForSeconds(1);

		if (!hasShield)
		{
			yield return new WaitForSeconds(1);
			EventSystemManager.Instance.EnableInput();
			RegisterScore();
			DialogueManager.Instance.ShowDialogue("GAME OVER", "Your score is: " + GameManager.Instance.gameInstance.score, okAction: () => {
				HandleGameOver();
			});
		}
		else
		{
			yield return new WaitForSeconds(1);
			DeactivateShield();
			onNextQuestion?.Invoke();
		}

		consecutiveCorrectAnswers = 0;
	}
	void HandleGameOver()
	{
		SceneManager.Instance.LoadMenu();
	}
	void RegisterScore()
	{
		if(PlayerPrefs.GetInt("Score") < score)
			PlayerPrefs.SetInt("Score",score);
	}
	private void OnTimeOut()
	{
		score -= PersistentManagers.Instance.settings.timePenalty;
		StartCoroutine(OnTimeOutRoutine());
	}
	IEnumerator OnTimeOutRoutine()
	{
		powerUps.RemoveArmors();
		FXManager.Instance.ShowFX(EFxType.TimeOutFX);

		if (hasShield)
			powerUps.DisableShied();

		yield return new WaitForSeconds(1);

		if (!hasShield)
		{
			yield return new WaitForSeconds(1);
			EventSystemManager.Instance.EnableInput();
			DialogueManager.Instance.ShowDialogue("GAME OVER", "Your score is: " + GameManager.Instance.gameInstance.score, okAction: () => {
				HandleGameOver();
			});
		}
		else
		{
			yield return new WaitForSeconds(1);
			DeactivateShield();
			onNextQuestion?.Invoke();
		}

		consecutiveCorrectAnswers = 0;
	}
}
