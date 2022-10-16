using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceController : MonoBehaviour
{
    public static bool racePending;
    public static int totalLaps = 1;

    public int timer = 3;
    CheckpointController[] controllers;

    private void Start()
    {
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
            print("Race Finished!");
            racePending = false;
        }
    }

    void CountDown()
    {
        if (timer > 0)
        {
            Debug.Log("Rozpoczêcie wyœcigu za: " + timer);
            timer--; 
        }
        else
        {
            print("Start!");
            racePending = true;
            CancelInvoke(nameof(CountDown));
        }
    }
}
