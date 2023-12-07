using EnhancedUI.EnhancedScroller;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CellView : EnhancedScrollerCellView
{
    public TextMeshProUGUI nickname;
    public TextMeshProUGUI score;
    public TextMeshProUGUI rank;
    public GameObject localPlayerFrame;
    public Transform medals;
    public bool localPlayerShown;

    public void SetData(LeaderboardElementData data)
    {
        nickname.text = data.nickname;
        score.text = data.score;
        rank.text = data.rank;
        localPlayerFrame.SetActive(data.isLocalPlayer);

        foreach (Transform child in medals)
		{
            child.gameObject.SetActive(false);
		}

        switch (Convert.ToInt32(data.rank))
		{
            case 1:
                medals.GetChild(0).gameObject.SetActive(true);
                break;
            case 2:
                medals.GetChild(1).gameObject.SetActive(true);
                break;
            case 3:
                medals.GetChild(2).gameObject.SetActive(true);
                break;
            default:
                medals.GetChild(3).gameObject.SetActive(true);
                break;
        }
    }
}