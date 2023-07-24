using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float thrustSpeed = 15f;
    public float rotationSpeed = 1;

    public float walkSpeed = 15f;
    public float jumpForce = 100f;

    private Rigidbody rb;
    private GravityObject gravityObject;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        gravityObject = gameObject.GetComponent<GravityObject>();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject.Find("UI_Speed").GetComponent<TMPro.TextMeshProUGUI>().text = "Speed: " + rb.velocity.magnitude.ToString();
    }

    void FixedUpdate()
    {
        if (gravityObject.onSurface)
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

        // Rotate
        bool shift = Input.GetKey(KeyCode.LeftShift);
        bool tiltLeft = Input.GetKey(KeyCode.Q);
        if (tiltLeft)
        {
            Debug.Log("tilt left!");
            if (shift)
            {
                transform.Rotate(0, -rotationSpeed, 0);
            }
            else
            {
                transform.Rotate(0, 0, rotationSpeed);
            }
        }

        // Rotate
        bool tiltRight = Input.GetKey(KeyCode.E);
        if (tiltRight)
        {
            Debug.Log("tilt tiltRight!");
            if (shift)
            {
                transform.Rotate(0, rotationSpeed, 0);
            }
            else
            {
                transform.Rotate(0, 0, -rotationSpeed);

            }
        }
    }

    private void MoveWithFeet()
    {
        Vector3 moveDir = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).normalized;
        Vector3 targetMoveAmount = moveDir * walkSpeed;
        rb.MovePosition(rb.position + transform.TransformDirection(targetMoveAmount));

        // Up
        bool upInput = Input.GetKey(KeyCode.Space);
        if (upInput && gravityObject.onSurface)
        {
            Vector3 upForce = new Vector3(0f, jumpForce, 0f);
            rb.AddRelativeForce(upForce);
        }
    }
}
