using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class RaceLauncher : MonoBehaviourPunCallbacks
{
    public TMP_InputField inputName;

    byte maxPlayersPerRoom = 4;
    bool isConnecting;
    public TextMeshProUGUI networkText;
    string gameVersion = "2.0";

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        if(PlayerPrefs.HasKey("PlayerName"))
        {
            inputName.text = PlayerPrefs.GetString("PlayerName");
        }
    }

    public void SetName(string name)
    {
        PlayerPrefs.SetString("PlayerName", name);
    }

    public void Connect()
    {
        networkText.text = "";
        isConnecting = true;
        PhotonNetwork.NickName = inputName.text;
        if( PhotonNetwork.IsConnected )
        {
            networkText.text += "Joining Room...\n";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            networkText.text += "Connecting to Server...\n";
            PhotonNetwork.GameVersion = gameVersion;
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    #region Photon Callbacks
    public override void OnConnectedToMaster()
    {
        if(isConnecting)
        {
            networkText.text += "Connected to Server.\n";
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        networkText.text += "Failed to join a Random Room...\n";
        PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = maxPlayersPerRoom} );
    }

    public override void OnJoinedRoom()
    {
        networkText.text += "Joined Room with " + PhotonNetwork.CurrentRoom.PlayerCount + " players\n";
        PhotonNetwork.LoadLevel("RaceScene");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        networkText.text += "Disconnected bcuz of: " + cause + "\n";
        isConnecting = false;
    }
    #endregion
}
