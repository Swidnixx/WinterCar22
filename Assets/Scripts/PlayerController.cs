using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviourPunCallbacks
{
    public Collider bodyCollider;

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
        bool nitro = Input.GetKeyDown(KeyCode.LeftShift);

        if(driveScript.rb.velocity.magnitude > 1 || !RaceController.racePending)
        {
            lastTimeMoving = Time.time;
        }

        if(RaceController.racePending != true && RaceController.raceController == true)
        {
            acc = 0;
        }
        else
        {
            driveScript.NitroBoost(nitro);
        }

        driveScript.Drive(acc, brake, steer);

        if (checkpointController.lastPoint == null) return;
        //additional distance from last point check
        Vector3 relativeCarPos = Vector3.Scale(driveScript.rb.transform.position, new Vector3(1, 0, 1));
        Vector3 relateviceLastPointPos = Vector3.Scale(checkpointController.lastPoint.position, new Vector3(1, 0, 1));
        float distance = Vector3.Distance(relativeCarPos, relateviceLastPointPos);
        CarApperance ca = GetComponent<CarApperance>();
        Debug.Log(ca.playerName + ": " + distance);

        if (distance > 1)
        {
            if ((Time.time > lastTimeMoving + 3 || driveScript.rb.transform.position.y < 14))
            // || driveScript.rb.transform.up.y < 0)
            {
                driveScript.rb.transform.position =
                    checkpointController.lastPoint.position;

                driveScript.rb.transform.rotation = checkpointController.lastPoint.rotation;

                driveScript.rb.velocity = Vector3.zero;
                driveScript.rb.angularVelocity = Vector3.zero;
                lastTimeMoving = Time.time;

                photonView.RPC(nameof(SetRespawnLayer), RpcTarget.All, null);

            }
        }
    }

    [PunRPC]
    void SetRespawnLayer()
    {
        Start();
        driveScript.rb.gameObject.layer = 6;
        bodyCollider.gameObject.layer = 6;
        Invoke(nameof(SetDefaultLayer), 10);
    }

    [PunRPC]
    void SetDefaultLayer()
    {
        driveScript.rb.gameObject.layer = 0;
        bodyCollider.gameObject.layer = 0;
    }

    void ResetLayer()
    {
        photonView.RPC(nameof(SetDefaultLayer), RpcTarget.All, null);
    }
}
