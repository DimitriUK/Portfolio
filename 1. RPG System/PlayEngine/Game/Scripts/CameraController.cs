using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Camera mainCam;
    private Rigidbody rb;
    private int moveSpeed = 50;
    private int fastSpeed = 130;
    private int maxSpeed = 50;

    private void Start()
    {
        mainCam = Camera.main;
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized;
        }

        if (Input.anyKey)
            UpdateCameraController();


        if (Input.GetKey(KeyCode.LeftShift))
            moveSpeed = fastSpeed;
        else moveSpeed = 50;
    }

    public void UpdateCameraController()
    {
        float hor = Input.GetAxis("Horizontal");
        float ver = Input.GetAxis("Vertical");

        rb.AddForce(transform.right * ver * moveSpeed);
        rb.AddForce(transform.forward * hor * -moveSpeed);
    }
}
