using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionManager : Singleton<ConnectionManager>
{
	public bool CheckConnection(Action onHasConnection, Action onNoConection)
	{
        if (Application.internetReachability != NetworkReachability.NotReachable)
        {
            onHasConnection.Invoke();
            EventSystemManager.Instance.EnableInput();
            return true;
        }
        else
        {
            onNoConection.Invoke();
            EventSystemManager.Instance.EnableInput();
            return false;
        }
    }
}
