using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
            GameObject.Find("UI_Pull").GetComponent<TMPro.TextMeshProUGUI>().text = gameObject.transform.parent.gameObject.name;
            GameObject.Find("UI_Mass").GetComponent<TMPro.TextMeshProUGUI>().text = "Mass: " + gameObject.transform.parent.gameObject.GetComponent<Rigidbody>().mass.ToString();
            other.gameObject.GetComponent<PlayerMovement>().gravityTarget = gameObject.transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject.Find("UI_Pull").GetComponent<TMPro.TextMeshProUGUI>().text = "Space";
            GameObject.Find("UI_Mass").GetComponent<TMPro.TextMeshProUGUI>().text = "Mass: -";
            other.gameObject.GetComponent<PlayerMovement>().gravityTarget = null;
        }
    }
}
