using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuRocketController : MonoBehaviour
{
	public RectTransform target;
	public GameObject thrust;
	public void Launch(Action _action)
	{
		transform.DOMove(target.position,.5f).SetEase(Ease.InQuad).OnComplete(() => {
			Fader.Instance.FadeOut(target.position, _action);
		});
			
		transform.DOScale(2,.5f).SetEase(Ease.InQuad);
		thrust.SetActive(true);
	}
}
