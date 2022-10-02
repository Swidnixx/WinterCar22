using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    DrivingScript driveScript;

    private void Start()
    {
        driveScript = GetComponent<DrivingScript>();
    }

    private void Update()
    {
        float acc = Input.GetAxis("Vertical");
        float brake = Input.GetAxis("Jump");
        float steer = Input.GetAxis("Horizontal");

        driveScript.Drive(acc, brake, steer);
    }
}
