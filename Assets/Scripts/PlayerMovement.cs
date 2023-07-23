using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform gravityTarget;
    public float speed = 15f;
    public float gravity = 9.81f;
    public bool inAtmosphere = true;
    public bool onSurface = true;

    public float autoOrientSpeed = 10f;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        ProcessInput();

        if (gravityTarget != null)
        {
            ProcessGravity();
        }
    }

    private void ProcessGravity()
    {
        Vector3 diff = transform.position - gravityTarget.position;
        rb.AddForce(-diff.normalized * gravity * (rb.mass));

        if (onSurface)
        {
            AutoOrient(-diff);
        }
    }

    private void ProcessInput()
    {
        // Forward/backwards
        float vt = Input.GetAxis("Vertical");
        Vector3 force = new Vector3(0f, 0f, vt * speed);
        rb.AddRelativeForce(force);

        // Left/right
        float hz = Input.GetAxis("Horizontal");
        Vector3 rforce = new Vector3(hz * speed, 0f, 0f);
        rb.AddRelativeForce(rforce);

        // Up
        bool upInput = Input.GetKey(KeyCode.Space);
        if (upInput)
        {
            Vector3 upForce = new Vector3(0f, speed, 0f);
            rb.AddRelativeForce(upForce);
        }

        // Down
        bool downInput = Input.GetKey(KeyCode.LeftShift);
        if (downInput)
        {
            Vector3 downForce = new Vector3(0f, -speed, 0f);
            rb.AddRelativeForce(downForce);
        }
    }

    private void AutoOrient(Vector3 down)
    {
        Quaternion orientationDirection = Quaternion.FromToRotation(-transform.up, down) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, orientationDirection, autoOrientSpeed * Time.deltaTime);
    }
}
