using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform gravityTarget;
    public float thrustSpeed = 15f;
    public float walkSpeed = 15f;
    public float gravity = 9.81f;
    public bool inAtmosphere = true;
    public bool onSurface = true;

    public bool enablePreviewMovement = true;

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
        GameObject.Find("UI_Speed").GetComponent<TMPro.TextMeshProUGUI>().text = "Speed: " + rb.velocity.magnitude.ToString();
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
        var targetMass = gravityTarget.gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().mass;

        Vector3 diff = transform.position - gravityTarget.position;
        Vector3 direction = -diff.normalized;
        float distance = diff.magnitude;

        float forceMagnitude = (gravity * rb.mass * targetMass) / Mathf.Pow(distance, 2);
        rb.AddForce(forceMagnitude * direction);

        if (onSurface)
        {
            AutoOrient(-diff);
        }
    }

    private void ProcessInput()
    {
        if (enablePreviewMovement && onSurface)
        {
            MoveWithFeet();
        }
        else
        {
            MoveWithThrust();
        }
    }

    private void MoveWithThrust()
    {
        // Forward/backwards
        float vt = Input.GetAxis("Vertical");
        Vector3 force = new Vector3(0f, 0f, vt * thrustSpeed);
        rb.AddRelativeForce(force);

        // Left/right
        float hz = Input.GetAxis("Horizontal");
        Vector3 rforce = new Vector3(hz * thrustSpeed, 0f, 0f);
        rb.AddRelativeForce(rforce);

        // Up
        bool upInput = Input.GetKey(KeyCode.Space);
        if (upInput)
        {
            Vector3 upForce = new Vector3(0f, thrustSpeed, 0f);
            rb.AddRelativeForce(upForce);
        }

        // Down
        bool downInput = Input.GetKey(KeyCode.LeftShift);
        if (downInput)
        {
            Vector3 downForce = new Vector3(0f, -thrustSpeed, 0f);
            rb.AddRelativeForce(downForce);
        }
    }

    private void MoveWithFeet()
    {
        Vector3 result = Vector3.zero;
        // Forward/backwards
        float vt = Input.GetAxis("Vertical");
        Vector3 force = new Vector3(0f, 0f, vt);
        result = force;

        // Left/right
        float hz = Input.GetAxis("Horizontal");
        Vector3 rforce = new Vector3(hz, 0f, 0f);
        result += rforce;

        // Up
        bool upInput = Input.GetKey(KeyCode.Space);
        if (upInput)
        {
            Vector3 upForce = new Vector3(0f, 1, 0f);
            result += upForce;
        }

        // Down
        bool downInput = Input.GetKey(KeyCode.LeftShift);
        if (downInput)
        {
            Vector3 downForce = new Vector3(0f, -1, 0f);
            result += downForce;
        }


        //rb.AddRelativeForce(result, mode: ForceMode.VelocityChange);
        if (result != Vector3.zero)
        {
            var velocity = result * walkSpeed;
            Vector3 relativeVelocity = Quaternion.Inverse(transform.rotation) * velocity;
            rb.velocity = relativeVelocity;
        }
        else
        {
            rb.velocity = Vector3.zero;
        }

        //rb.transform.position = Vector3.MoveTowards(rb.transform.position, rb.transform.position + result, speed * Time.deltaTime);
    }

    private void AutoOrient(Vector3 down)
    {
        Quaternion orientationDirection = Quaternion.FromToRotation(-transform.up, down) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, orientationDirection, autoOrientSpeed * Time.deltaTime);
    }
}
