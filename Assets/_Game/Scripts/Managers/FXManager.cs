using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXManager : Singleton<FXManager>
{
	public UIAnimation winEffect;
	public UIAnimation loseEffect;
	public UIAnimation timeOutEffect;
	[SerializeField]
	private Canvas canvas;
	private void Start()
	{
		winEffect.gameObject.SetActive(false);
		loseEffect.gameObject.SetActive(false);
		timeOutEffect.gameObject.SetActive(false);

		canvas.worldCamera = Camera.main;
		canvas.planeDistance = 3;
	}
	public void ShowFX(EFxType type)
	{
		switch (type)
		{
			case EFxType.WinEffect:
				CorrectAnswerEffect();
				break;
			case EFxType.LoseEffect:
				WrongAnswerEffect();
				break;
			case EFxType.TimeOutFX:
				TimeOutEffect();
				break;
		}
	}
	void CorrectAnswerEffect()
	{
		winEffect.Enable(endAction: () => {
			winEffect.Disable();
		});
	}
	void WrongAnswerEffect()
	{
		loseEffect.Enable(endAction: () => {
			loseEffect.Disable();
		});
	}
	void TimeOutEffect()
	{
		timeOutEffect.Enable(endAction: () => {
			timeOutEffect.Disable();
		});
	}
}
public enum EFxType
{
	WinEffect,
	LoseEffect,
	TimeOutFX
}
