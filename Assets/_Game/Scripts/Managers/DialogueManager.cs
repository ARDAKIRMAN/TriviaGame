using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : Singleton<DialogueManager>
{
	public UIAnimation content;
	public Button okButton;
	public TextMeshProUGUI txtHeader;
	public TextMeshProUGUI txtMessage;
	[SerializeField]
	private Canvas canvas;
	private void Start()
	{
		content.gameObject.SetActive(false);

		canvas.worldCamera = Camera.main;
		canvas.planeDistance = 2;
	}
	public void ShowDialogue(string header,string message = "" , Action okAction = null)
	{
		content.Enable();
		txtHeader.text = header;
		txtMessage.text = message;
		okButton.onClick.RemoveAllListeners();

		okButton.onClick.AddListener(() => {
			HideDialogue();
			SoundManager.Instance.PlayButon();
			okAction?.Invoke();
		});

		content.Enable();
	}
	public void HideDialogue()
	{
		content.Disable();
	}
}
