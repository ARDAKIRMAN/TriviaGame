using DG.Tweening;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIAnimation : MonoBehaviour
{
	public bool autoAnimation = true;
	public bool useInAnimaton;
	[ShowIf("useInAnimaton")]
	public AnimationSytle inAnimation;
	public bool useOutAnimaton;
	[ShowIf("useOutAnimaton")]
	public AnimationSytle outAnimation;
	CanvasGroup _cg;
	RectTransform rect;
	Vector2 originalPosition;
	private void Awake()
	{
		rect = GetComponent<RectTransform>();
		originalPosition = rect.anchoredPosition;
	}
	private void OnEnable()
	{
		if (autoAnimation) Enable();	
	}
	public void Enable(Action startAction = null, Action endAction = null)
	{
		if (useInAnimaton)
		{
			gameObject.SetActive(true);
			inAnimation.onStart = startAction;
			inAnimation.onEnd = endAction;

			switch (inAnimation.type)
			{
				case UIAnimationType.Left:
					FromLeft();
					break;
				case UIAnimationType.Right:
					FromRight();
					break;
				case UIAnimationType.Bottom:
					FromBottom();
					break;
				case UIAnimationType.Top:
					FromTop();
					break;
				case UIAnimationType.Fade:
					FadeIn();
					break;
				default:
					break;
			}
		}
	}
	public void Disable(Action startAction = null, Action endAction = null)
	{
		gameObject.SetActive(true);

		outAnimation.onStart = startAction;
		outAnimation.onEnd = endAction;

		switch (outAnimation.type)
		{
			case UIAnimationType.Left:
				ToLeft();
				break;
			case UIAnimationType.Right:
				ToRight();
				break;
			case UIAnimationType.Bottom:
				ToBottom();
				break;
			case UIAnimationType.Top:
				ToTop();
				break;
			case UIAnimationType.Fade:
				FadeOut();
				break;
			default:
				break;
		}
	}
	void CheckCanvasGroup()
	{
		if (!gameObject.TryGetComponent(out _cg))
		{
			_cg = gameObject.AddComponent<CanvasGroup>();
		}
	}
	void FromTop()
	{
		CheckCanvasGroup();
		_cg.alpha = 0;
		rect.anchoredPosition = new Vector2(originalPosition.x, 1000);
		rect.DOAnchorPos(originalPosition, inAnimation.time, false).SetEase(Ease.OutFlash).OnComplete(() => {
			inAnimation.onEnd?.Invoke();
		});
		_cg.DOFade(1,inAnimation.time);
	}
	void ToTop()
	{
		CheckCanvasGroup();
		rect.DOAnchorPos(new Vector2(originalPosition.x, 1000), inAnimation.time, false).SetEase(Ease.OutFlash).OnComplete(() => {
			outAnimation.onEnd?.Invoke();
		});
		_cg.DOFade(0, inAnimation.time);
	}
	void FromRight()
	{
		CheckCanvasGroup();
		_cg.alpha = 0;
		rect.anchoredPosition = new Vector3(1000, originalPosition.y, 0);
		rect.DOAnchorPos(originalPosition, inAnimation.time, false).SetEase(Ease.OutFlash).OnComplete(() => {
			inAnimation.onEnd?.Invoke();
		});
		_cg.DOFade(1, inAnimation.time);
	}
	void ToRight()
	{
		CheckCanvasGroup();
		rect.DOAnchorPos(new Vector3(1000, originalPosition.y, 0), inAnimation.time, false).SetEase(Ease.OutFlash).OnComplete(() => {
			outAnimation.onEnd?.Invoke();
		});
		_cg.DOFade(0, inAnimation.time);
	}
	void FromLeft()
	{
		CheckCanvasGroup();
		_cg.alpha = 0;
		rect.anchoredPosition = new Vector3(-1000, originalPosition.y, 0);
		rect.DOAnchorPos(originalPosition, inAnimation.time, false).SetEase(Ease.OutFlash).OnComplete(() => {
			inAnimation.onEnd?.Invoke();
		});
		_cg.DOFade(1, inAnimation.time);
	}
	void ToLeft()
	{
		CheckCanvasGroup();
		rect.DOAnchorPos(new Vector3(-1000, originalPosition.y, 0), inAnimation.time, false).SetEase(Ease.OutFlash).OnComplete(() => {
			outAnimation.onEnd?.Invoke();
		});
		_cg.DOFade(0, inAnimation.time);
	}
	void FromBottom()
	{
		CheckCanvasGroup();
		_cg.alpha = 0;
		rect.anchoredPosition = new Vector3(originalPosition.x, -1000, 0);
		rect.DOAnchorPos(originalPosition, inAnimation.time, false).SetEase(Ease.OutFlash).OnComplete(() => {
			inAnimation.onEnd?.Invoke();
		});
		_cg.DOFade(1, inAnimation.time);
	}
	void ToBottom()
	{
		CheckCanvasGroup();
		rect.DOAnchorPos(new Vector3(originalPosition.x, -1000, 0), inAnimation.time, false).SetEase(Ease.OutFlash).OnComplete(() => {
			outAnimation.onEnd?.Invoke();
		});
		_cg.DOFade(1, inAnimation.time);
	}
	void FadeIn()
	{
		CheckCanvasGroup();
		_cg.alpha = 0;
		_cg.DOFade(1, inAnimation.time).OnComplete(() => {
			inAnimation.onEnd?.Invoke();
		});
	}
	void FadeOut()
	{
		CheckCanvasGroup();
		_cg.DOFade(0, inAnimation.time).OnComplete(() => {
			outAnimation.onEnd?.Invoke();

			gameObject.SetActive(false);
		});
	}
	[System.Serializable]
	public class AnimationSytle
	{
		public UIAnimationType type;
		public float time;
		public Action onStart, onEnd;
	}
}
public enum UIAnimationType
{
	Left,
	Right,
	Bottom,
	Top,
	Fade
}
