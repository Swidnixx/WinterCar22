using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class RaceController : MonoBehaviourPunCallbacks
{
    //Game Logic Fields
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

    //Network Logic Fields
    public GameObject startRaceButton;
    public GameObject waitingForStartText;

    private void Start()
    {
        //Spawnowanie graczy
        for(int i=0; i<playerCount; i++)
        {
            GameObject car = Instantiate(carPrefab);
            car.transform.position = spawnPositions[i].position;
            car.transform.rotation = spawnPositions[i].rotation;
            car.GetComponent<CarApperance>().playerNumber = i;
            if( i == 0)
            {
                car.GetComponent<PlayerController>().enabled = true;
                GameObject.FindObjectOfType<CameraController>().SetCamera(car.transform);
            }
        }

        endGamePanel.SetActive(false);

        audioSource = GetComponent<AudioSource>();

        GameObject[] cars = GameObject.FindGameObjectsWithTag("Car");
        controllers = new CheckpointController[cars.Length];
        for(int i=0; i< cars.Length; i++)
        {
            controllers[i] = cars[i].GetComponent<CheckpointController>();
        }

        InvokeRepeating(nameof(CountDown), 3, 1);
    }

    private void LateUpdate()
    {
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
