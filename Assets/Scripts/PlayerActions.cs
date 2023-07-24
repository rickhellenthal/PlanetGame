using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : MonoBehaviour
{
    private GameObject torch;

    // Start is called before the first frame update
    void Start()
    {
        torch = GameObject.Find("Torch");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            torch.SetActive(!torch.activeSelf);
        }
    }
}
