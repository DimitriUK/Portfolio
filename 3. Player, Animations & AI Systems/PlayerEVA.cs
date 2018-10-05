using UnityEngine;

public class PlayerEVA : MonoBehaviour
{
    public Rigidbody rb;
    public float thrust;
    private float maxSpeed = 4;

    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = 2.0F;

    void FixedUpdate()
    {
        //All Inputs to be referenced to buttons at a later stage (Placeholder Inputs)

        if (Input.GetKey(KeyCode.Space))
        {
            rb.AddForce(transform.up * thrust);
        }
        if (Input.GetKey(KeyCode.C))
        {
            rb.AddForce(-transform.up * thrust);
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward * thrust);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(-transform.forward * thrust);
        }
        if (Input.GetKey(KeyCode.A))
        {
            rb.AddForce(-transform.right * thrust);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddForce(transform.right * thrust);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            rb.AddTorque(transform.forward * thrust / 3);
        }
        if (Input.GetKey(KeyCode.E))
        {
            rb.AddTorque(-transform.forward * thrust / 3);
        }

        float h = horizontalSpeed * Input.GetAxis("Mouse X");
        float v = verticalSpeed * Input.GetAxis("Mouse Y");

        rb.AddTorque(transform.up * h);
        rb.AddTorque(-transform.right * v);
    }
}
