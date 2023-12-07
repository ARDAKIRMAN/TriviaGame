using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : Singleton<Fader>
{
	public RectTransform circleFade;
	public CanvasGroup screenFade;
	[SerializeField]
	private Canvas canvas;
	private void Start()
	{
		canvas.worldCamera = Camera.main;
		canvas.planeDistance = 1;
	}
	public void FadeIn()
	{
		circleFade.localScale = Vector3.zero;
		screenFade.DOFade(0,1);
	}
	public void FadeOut(Vector2 _position, Action _action)
	{
		screenFade.alpha = 0;
		circleFade.localScale = Vector3.zero;
		circleFade.position = _position;
		circleFade.DOScale(100, 1).OnComplete(() => {
			_action.Invoke();
		});
	}
	public void FadeOut()
	{
		circleFade.localScale = Vector3.zero;
		screenFade.DOFade(1, 1);
	}
}
