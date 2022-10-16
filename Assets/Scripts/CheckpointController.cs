using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public int checkpoint = -1;
    public int lap = 0;

    int checkpointCount;
    int nextCheckpoint = 0;

    private void Start()
    {
        GameObject[] checkpointObjects = GameObject.FindGameObjectsWithTag("Checkpoint");
        checkpointCount = checkpointObjects.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Checkpoint"))
        {
            int thisCheckpoint = int.Parse(other.name);
            if(thisCheckpoint == nextCheckpoint)
            {
                checkpoint = thisCheckpoint;
                if(checkpoint == 0)
                {
                    lap++;
                    print("Lap: " + lap);
                }
                nextCheckpoint++;
                nextCheckpoint = nextCheckpoint % checkpointCount;
            }
        }
    }
}
