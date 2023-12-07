using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettings : ScriptableObject
{
	public int correctReward = 10;
	public int wrongPenalty = -5;
	public int timePenalty = -3;
	public float questionTime = 20;
	public int requiredArmorForShield = 3;
	public int targetedFPS = 60;
}
