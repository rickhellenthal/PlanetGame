using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullRadius : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Entering gravitational pull: " + gameObject.name);
            other.gameObject.GetComponent<PlayerMovement>().gravityTarget = gameObject.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Leaving: " + gameObject.name);
            other.gameObject.GetComponent<PlayerMovement>().gravityTarget = null;
        }
    }
}
