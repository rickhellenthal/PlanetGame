using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class PlayerCameraMovement : MonoBehaviour
{
    public float sensitivity;
    public Camera _camera;
    public float maxLookAngle = 90f;
    public bool invertCamera;

    private GravityObject gravityObject;
    private Transform cameraTransform;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        gravityObject = gameObject.GetComponent<GravityObject>();
        cameraTransform = _camera.GetComponent<Transform>();
    }

    void Update()
    {
        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = -Input.GetAxis("Mouse Y");

        transform.Rotate(0f, rotateHorizontal * sensitivity, 0f);

        if (gravityObject.onSurface)
        {
            cameraTransform.Rotate(rotateVertical * sensitivity, 0f, 0f);
        }
        else
        {
            transform.Rotate(rotateVertical * sensitivity, 0f, 0f);
        }
    }
}
