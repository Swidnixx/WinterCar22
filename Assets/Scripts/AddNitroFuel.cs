using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddNitroFuel : MonoBehaviour
{
    public PlayerController ps;
    public DrivingScript ds;

    internal void AddFuel()
    {
        if (ps.enabled)
        {
            ds.nitroFuel++;
            ds.SetFuelUI();
        }
    }
}
