using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    float lastTimeMoving = 0;
    CheckpointController checkpointController;

    DrivingScript driveScript;

    private void Start()
    {
        driveScript = GetComponent<DrivingScript>();
        checkpointController = driveScript.rb.GetComponent<CheckpointController>();
    }

    private void Update()
    {
        float acc = Input.GetAxis("Vertical");
        float brake = Input.GetAxis("Jump");
        float steer = Input.GetAxis("Horizontal");

        if(driveScript.rb.velocity.magnitude > 1 || !RaceController.racePending)
        {
            lastTimeMoving = Time.time;
        }

        if(Time.time > lastTimeMoving + 3 || driveScript.rb.transform.position.y < 14 )
           // || driveScript.rb.transform.up.y < 0)
        {
            driveScript.rb.transform.position =
                checkpointController.lastPoint.position;

            driveScript.rb.transform.rotation = checkpointController.lastPoint.rotation;

            driveScript.rb.velocity = Vector3.zero;
            driveScript.rb.angularVelocity = Vector3.zero;
            lastTimeMoving = Time.time;

            driveScript.rb.gameObject.layer = 6;
            Invoke(nameof(ResetLayer), 3);

        }

        if(RaceController.racePending != true)
        {
            acc = 0;
        }

        driveScript.Drive(acc, brake, steer);
    }

    void ResetLayer()
    {
        driveScript.rb.gameObject.layer = 0; 
    }
}
