using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using UnityEngine.UI;

public class RaceController : MonoBehaviourPunCallbacks
{
    //Game Logic Fields
    public static bool raceController;
    public static bool racePending;
    public static int totalLaps = 1;

    public int timer = 3;
    CheckpointController[] controllers;

    public TextMeshProUGUI startText;
    public GameObject endGamePanel;

    AudioSource audioSource;
    public AudioClip countClip;
    public AudioClip startClip;

    public GameObject carPrefab;
    public Transform[] spawnPositions;
    public int playerCount = 2;

    public RawImage mirrorImage;

    //Network Logic Fields
    public GameObject startRaceButton;
    public GameObject waitingForStartText;

    private void Awake()
    {
        raceController = true;
    }

    public void SetMirrorPic(Camera backCamera)
    {
        mirrorImage.texture = backCamera.targetTexture;
    }

    [PunRPC]
    public void StartRace()
    {
        startRaceButton.SetActive(false);
        waitingForStartText.SetActive(false);

        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        controllers = new CheckpointController[cars.Length];
        for (int i = 0; i < cars.Length; i++)
        {
            controllers[i] = cars[i].GetComponent<CheckpointController>();
        }

        InvokeRepeating(nameof(CountDown), 3, 1);
    }

    public void StartRaceButton()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            photonView.RPC(nameof(StartRace), RpcTarget.All, null);
        }
    }

    private void Start()
    {
        endGamePanel.SetActive(false);
        audioSource = GetComponent<AudioSource>();

        //Spawnowanie graczy
        playerCount = PhotonNetwork.CurrentRoom.PlayerCount;


        startRaceButton.SetActive(false);
        waitingForStartText.SetActive(false);

        Vector3 spawnPos;
        Quaternion spawnRot;
        GameObject playerCar = null;

        if(PhotonNetwork.IsConnected)
        {
            spawnPos = spawnPositions[playerCount - 1].position;
            Debug.Log("Spawn pos:" + spawnPos);
            spawnRot = spawnPositions[playerCount - 1].rotation;

            object[] instanceData = new object[4];
            instanceData[0] = PlayerPrefs.GetString("PlayerName");
            instanceData[1] = PlayerPrefs.GetInt("Red");
            instanceData[2] = PlayerPrefs.GetInt("Green");
            instanceData[3] = PlayerPrefs.GetInt("Blue");

            if(OnlinePlayer.LocalPlayerInstance == null)
            {
                playerCar = PhotonNetwork.Instantiate(carPrefab.name, spawnPos, spawnRot, 0, instanceData);
                playerCar.GetComponent<CarApperance>().SetLocalPlayer();
            }

            if(PhotonNetwork.IsMasterClient)
            {
                startRaceButton.SetActive(true);
            }
            else
            {
                waitingForStartText.SetActive(true);
            }
        }

        playerCar.GetComponent<PlayerController>().enabled = true;
    }

    private void LateUpdate()
    {
        if (!racePending) return;

        int finishers = 0;
        foreach(CheckpointController c in controllers)
        {
            if (c.lap == totalLaps + 1) finishers++;
        }

        if(finishers == controllers.Length && racePending)
        {
            endGamePanel.SetActive(true);
            racePending = false;
        }
    }

    public void RestartRace()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void CountDown()
    {
        if (timer > 0)
        {
            audioSource.PlayOneShot(countClip); 
            startText.text = ("Rozpoczêcie wyœcigu za: " + timer);
            timer--; 
        }
        else
        {
            audioSource.PlayOneShot(startClip);
            startText.text = ("Start!");
            racePending = true;
            CancelInvoke(nameof(CountDown));

            Invoke(nameof(HideStartText), 1);
        }
    }
    void HideStartText()
    {
        startText.gameObject.SetActive(false);
    }
}
