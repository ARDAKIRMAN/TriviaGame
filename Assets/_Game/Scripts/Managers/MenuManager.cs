using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
	public GameObject canvasMainMenu;
	public MenuRocketController rocket;
	public TextMeshProUGUI txtScore;
	private void Start()
	{
		QualitySettings.vSyncCount = 0;
		Application.targetFrameRate = PersistentManagers.Instance.settings.targetedFPS;
		SetScore();
		
		Fader.Instance.FadeIn();
	}
	public void BtnPlay()
	{
		EventSystemManager.Instance.DisableInput();
		ConnectionManager.Instance.CheckConnection(
			() => {
				rocket.Launch(() => {
					SceneManager.Instance.LoadGame();
					EventSystemManager.Instance.EnableInput();
				});
			},
			() => {
				ShowConnectionError();
				EventSystemManager.Instance.EnableInput();
			}
			);
	}
	public void BtnLeaderboards_Open()
	{
		EventSystemManager.Instance.DisableInput();
		LeaderboardManager.Instance.isLocalPlayerShown = false;
		ConnectionManager.Instance.CheckConnection(
			() => {
				LeaderboardManager.Instance.LoadPage(() => {
					canvasMainMenu.SetActive(false);
					LeaderboardManager.Instance.content.SetActive(true);
					EventSystemManager.Instance.EnableInput();
				});
			},
			() => {
				ShowConnectionError();
			}
			);
	}
	public void BtnLeaderboards_Close()
	{
		LeaderboardManager.Instance.content.SetActive(false);
		canvasMainMenu.SetActive(true);
		LeaderboardManager.Instance.ClearData();
	}
	void SetScore()
	{
		txtScore.text = "Highest Score : " + PlayerPrefs.GetInt("Score");
	}
	void ShowConnectionError()
	{
		DialogueManager.Instance.ShowDialogue("NO INTERNET CONNECTION", "This application requires an Internet connection to work.");
	}
}
