using UnityEngine;

public class QuestionTimer : MonoBehaviour
{
	public UIAnimation uiAnimation;
	public float timer;
	bool isRunning;
	float startTime;
	bool timeoutShown;
	private void Start()
	{
		uiAnimation.gameObject.SetActive(false);
	}
	public void UpdateTimer()
	{
		if (!isRunning) return;

		timer = Mathf.Clamp(PersistentManagers.Instance.settings.questionTime - (Time.realtimeSinceStartup - startTime), 0, float.MaxValue);

		if (timer == 0 && !timeoutShown)
		{
			timeoutShown = true;
			GameManager.Instance.gameInstance.onTimeOut?.Invoke();
		}
	}
	public void StartTimer()
	{
		timeoutShown = false;
		isRunning = true;
		startTime = Time.realtimeSinceStartup;
		timer = PersistentManagers.Instance.settings.questionTime;
	}
	public void StopTimer()
	{
		isRunning = false;
	}
	public void ShowTimer()
	{
		uiAnimation.Enable();
	}
	public void HideTimer()
	{
		uiAnimation.Disable();
	}
}
