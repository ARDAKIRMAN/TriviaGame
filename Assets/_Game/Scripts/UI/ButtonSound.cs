using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour //Do not put this script on buttons that you add/remove listeners from another script
{
	private void Start()
	{
		if(TryGetComponent(out Button _button))
		{
			_button.onClick.AddListener(()=> { SoundManager.Instance.PlayButon(); });
		}
	}
}
