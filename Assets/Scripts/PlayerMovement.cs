using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float thrustSpeed = 15f;

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
