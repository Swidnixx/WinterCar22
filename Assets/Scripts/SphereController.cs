using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereController : MonoBehaviour
{
    Rigidbody rb;
    Vector3 input;

    public float power = 10;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveY = Input.GetAxis("Vertical");

        input = new Vector3(moveX, 0, moveY).normalized;
    }

    private void FixedUpdate()
    {
        rb.AddForce(input * power);
    }
}
