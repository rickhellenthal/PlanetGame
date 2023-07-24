using UnityEngine;

public class GravityObject : MonoBehaviour
{
    public Transform gravityTarget;
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

    private void AutoOrient(Vector3 down)
    {
        Quaternion orientationDirection = Quaternion.FromToRotation(-transform.up, down) * transform.rotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, orientationDirection, autoOrientSpeed * Time.deltaTime);
    }
}
