using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeaderboardPanel : MonoBehaviour
{
    public TextMeshProUGUI[] texts;

    private void Start()
    {
        Leaderboard.Reset();
    }

    private void LateUpdate()
    {
        List<string> playersSorted = Leaderboard.GetPlaces();

        for(int i=0; i<texts.Length; i++)
        {
            if(i<playersSorted.Count)
            {
                texts[i].text = playersSorted[i];
            }
            else
            {
                texts[i].text = "";
            }
        }
    }
}
