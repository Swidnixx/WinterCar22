using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class RaceLauncher : MonoBehaviour
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

    public void StartTestRace()
    {
        SceneManager.LoadScene("RaceScene");
    }
}
