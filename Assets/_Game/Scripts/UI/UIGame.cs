using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIGame : MonoBehaviour
{
	public TextMeshProUGUI txtQuestion;
	public List<BtnAnswer> answers;
	public UIAnimation questionAnim;
	public UIAnimation answersAnim;
	public Image timer;
	public Sprite defaultSprite, correctSprite, wrongSprite;
	public GameObject content;
	public QuestionTimer questionTimer;
	public TextMeshProUGUI txtScore;

	public void Start()
	{
		Hide(true);
		GameManager.Instance.gameInstance.onPrepareGame += OnPrepareGame;
		GameManager.Instance.gameInstance.onNextQuestion += NextQuestion;
		GameManager.Instance.gameInstance.onTimeOut += OnTimeOut;
	}
	private void Update()
	{
		questionTimer.UpdateTimer();
		SetTimerUI();
	}
	void SetTimerUI()
	{
		timer.fillAmount = questionTimer.timer / PersistentManagers.Instance.settings.questionTime;
		timer.color = Color.Lerp(Color.red * 2, Color.green, timer.fillAmount);
	}
	public void OnPrepareGame()
	{
		SetScore();
		GameManager.Instance.gameInstance.onNextQuestion.Invoke();
	}
	public void OnTimeOut()
	{
		Hide();
		SetScore();
		questionTimer.HideTimer();
	}
	public void Show()
	{
		questionAnim.Enable();
		answersAnim.Enable();
	}
	public void Hide(bool _immidiate = false)
	{
		if(_immidiate)
		{
			questionAnim.gameObject.SetActive(false);
			answersAnim.gameObject.SetActive(false);
		}
		else
		{
			questionAnim.Disable();
			answersAnim.Disable();
		}

	}
	public void FillQuestion(Question _question)
	{
		txtQuestion.text = _question.question.question;

		BtnAnswer correctAnswer = null;

		for (int i = 0; i < answers.Count; i++)
		{
			var _answer = answers[i];
			_answer.button.onClick.RemoveAllListeners();

			_answer.text.text = _question.question.options[i].Substring(3);

			if (_question.question.options[i].Substring(0, 1) == _question.question.answer.Substring(0, 1))
			{
				correctAnswer = _answer;
				answers[i].button.onClick.AddListener(() => {
					CorrectAnswer(correctAnswer);
				});
			}
		}

		for (int i = 0; i < answers.Count; i++)
		{
			var _answer = answers[i];
			if (_answer != correctAnswer)
			{
				_answer.button.onClick.AddListener(() => {
					WrongAnswer(_answer, correctAnswer);
				});
			}
		}

		ResetAnswerSprites();
		questionTimer.ShowTimer();
		gameObject.SetActive(true);
	}
	void SetScore()
	{
		txtScore.text = "Score : " + GameManager.Instance.gameInstance.score.ToString();
	}
	public void NextQuestion()
	{
		StartCoroutine(NextQuestionRoutine());
	}
	IEnumerator NextQuestionRoutine()
	{
		var waitForEndofFrame = new WaitForEndOfFrame();
		while (!QuestionManager.Instance.CheckQuestions())
		{
			yield return waitForEndofFrame;
		}

		FillQuestion(QuestionManager.Instance.GetQuestion());
		Show();
		GameManager.Instance.gameInstance.questionTimer.StartTimer();
		EventSystemManager.Instance.EnableInput();
	}
	void CorrectAnswer(BtnAnswer _btnAnswer)
	{
		Debug.Log("Correct");
		EventSystemManager.Instance.DisableInput();
		questionTimer.StopTimer();
		questionTimer.HideTimer();
		GameManager.Instance.gameInstance.onCorrectAnswer?.Invoke();
		StartCoroutine(CorrectRoutine(_btnAnswer));
		SoundManager.Instance.PlayCorrect();
		SetScore();
	}
	IEnumerator CorrectRoutine(BtnAnswer _btnAnswer)
	{
		_btnAnswer.button.image.sprite = correctSprite;

		yield return new WaitForSeconds(1);
		Hide();
		yield return new WaitForSeconds(1);
		GameManager.Instance.gameInstance.onNextQuestion?.Invoke();
	}
	void WrongAnswer(BtnAnswer _btnAnswer, BtnAnswer _correctAnswer)
	{
		Debug.Log("Wrong");
		EventSystemManager.Instance.DisableInput();
		questionTimer.StopTimer();
		questionTimer.HideTimer();
		GameManager.Instance.gameInstance.onWrongAnswer?.Invoke();
		StartCoroutine(WrongRoutine(_btnAnswer, _correctAnswer));
		SoundManager.Instance.PlayWrong();
		SetScore();
	}
	IEnumerator WrongRoutine(BtnAnswer _btnAnswer, BtnAnswer _correctAnswer)
	{
		_btnAnswer.button.image.sprite = wrongSprite;
		_correctAnswer.button.image.sprite = correctSprite;

		yield return new WaitForSeconds(1);
		Hide();
	}
	void ResetAnswerSprites()
	{
		foreach (var btn in answers)
		{
			btn.button.image.sprite = defaultSprite;
		}
	}
}
