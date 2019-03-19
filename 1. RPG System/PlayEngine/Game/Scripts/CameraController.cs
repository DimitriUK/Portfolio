using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera mainCam;
    private Rigidbody rb;
    private const int moveSpeed = 50;

    private void Start()
    {
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.anyKey)
            UpdateCameraController();
    }

    public void UpdateCameraController()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        rb.AddForce(transform.right * ver * moveSpeed);
        rb.AddForce(transform.forward * hor * -moveSpeed);
    }
}
