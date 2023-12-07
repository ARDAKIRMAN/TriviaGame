using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSystemManager : Singleton<EventSystemManager>
{
	[SerializeField]
	private EventSystem eventSystem;
	public void EnableInput()
	{
		eventSystem.enabled = true;
	}
	public void DisableInput()
	{
		eventSystem.enabled = false;
	}
}
