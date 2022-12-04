using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnlinePlayer : MonoBehaviourPunCallbacks
{
    public static GameObject LocalPlayerInstance;

    private void Awake()
    {
        if(photonView.IsMine)
        {
            LocalPlayerInstance = this.gameObject;
        }
        else
        {
            string playerName;
            Color playerColor;

            if(photonView.InstantiationData != null)
            {
                playerName = (string)photonView.InstantiationData[0];
                playerColor = MenuController.IntToColor((int)photonView.InstantiationData[1], (int)photonView.InstantiationData[2],
                    (int)photonView.InstantiationData[3]);

                GetComponent<CarApperance>().SetAvatar(playerName, playerColor);
            }
        }
    }
}
