using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DrivingScript : MonoBehaviour
{
    public WheelBase[] wheels;
    public Rigidbody rb;

    //Settings
    public float torque = 500;
    public float maxSteerAngle = 30;
    public float brakeTorque = 2000;
    public float maxSpeed = 100;

    //Debug
    public float currentSpeed;

    //Nitro
    public int nitroFuel = 5;
    TextMeshProUGUI nitroText;

    private void Start()
    {
        nitroText = GameObject.FindGameObjectWithTag("NitroText").GetComponent<TextMeshProUGUI>();
        SetFuelUI();
    }

    internal void SetFuelUI()
    {
        nitroText.text = nitroFuel.ToString();
    }

    public void NitroBoost(bool active)
    {
        if(active && nitroFuel > 0)
        {
            nitroFuel--;
            rb.AddForce(rb.gameObject.transform.forward * 10, ForceMode.VelocityChange);
            SetFuelUI();
        }
    }

    public void Drive(float ac, float brake, float steer)
    {
        ac = Mathf.Clamp(ac, -1, 1);
        steer = Mathf.Clamp(steer, -1, 1);
        brake = Mathf.Clamp(brake, 0, 1);

        float appliedTorque = 0;
        currentSpeed = rb.velocity.magnitude * 3.6f;
        if(currentSpeed < maxSpeed)
        {
            appliedTorque = ac * torque;
        }

        foreach(WheelBase wheel in wheels)
        {
            wheel.wheelCollider.motorTorque = appliedTorque;
            wheel.wheelCollider.brakeTorque = brake * brakeTorque;
            if(wheel.frontWheel)
            {
                wheel.wheelCollider.steerAngle = steer * maxSteerAngle;
            }
        }
    }
}
