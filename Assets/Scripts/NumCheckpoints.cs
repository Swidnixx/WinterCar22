using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class NumCheckpoints : MonoBehaviour
{
    private void Start()
    {
        Transform[] checkpoints = GetComponentsInChildren<Transform>();
        for(int i=1; i< checkpoints.Length; i++)
        {
            checkpoints[i].name = (i - 1).ToString();
        }
    }
}
