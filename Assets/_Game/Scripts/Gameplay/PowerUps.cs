using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public List<UIAnimation> uiArmorItems;
	public UIAnimation uisShield;
	int currentArmorCount;

	private void Start()
	{
		DisableShied(true);
		RemoveArmors();
	}

	public void AddArmor()
	{
		if (currentArmorCount < PersistentManagers.Instance.settings.requiredArmorForShield - 1)
		{
			uiArmorItems[currentArmorCount].Enable();
		}
		else if(currentArmorCount == PersistentManagers.Instance.settings.requiredArmorForShield - 1)
		{
			RemoveArmors();
			EnableShied();
		}
		Mathf.Clamp(currentArmorCount ++,0,3);
	}
	public void RemoveArmors()
	{
		foreach (var _armor in uiArmorItems)
		{
			_armor.Disable();
		}
	}
	public void EnableShied()
	{
		uisShield.Enable();
		GameManager.Instance.gameInstance.ActivateShield();
		SoundManager.Instance.PlayShield();
	}
	public void DisableShied(bool instant = false)
	{
		if (instant)
		{
			uisShield.gameObject.SetActive(false);
		}
		else
		{
			uisShield.Disable();
		}
		currentArmorCount = 0;
	}
}
