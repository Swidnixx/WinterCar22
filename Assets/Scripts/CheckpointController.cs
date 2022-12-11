using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    public int checkpoint = -1;
    public Transform lastPoint;
    public int lap = 0;

    int checkpointCount;
    int nextCheckpoint = 0;

    private void Start()
    {
        GameObject[] checkpointObjects = GameObject.FindGameObjectsWithTag("Checkpoint");
        checkpointCount = checkpointObjects.Length;
        for(int i=0; i < checkpointCount; i++)
        {
            if(checkpointObjects[i].name == "0")
            {
                lastPoint = checkpointObjects[i].transform;
                break;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Checkpoint"))
        {
            int thisCheckpoint = int.Parse(other.name);
            if(thisCheckpoint == nextCheckpoint)
            {
                checkpoint = thisCheckpoint;
                lastPoint = other.transform;

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
