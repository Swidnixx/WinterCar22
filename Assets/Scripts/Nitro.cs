using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nitro : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        AddNitroFuel script = other.GetComponent<AddNitroFuel>();
        if(script)
        {
            script.AddFuel();
        }
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0.4f, -0.4f, 0.4f));
    }
}
