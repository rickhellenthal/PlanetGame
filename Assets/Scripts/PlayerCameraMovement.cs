using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PlayerCameraMovement : MonoBehaviour
{
    public float sensitivity;
    public Camera _camera;
    public float maxLookAngle = 90f;
    public bool invertCamera;

    private Transform cameraTransform;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        cameraTransform = _camera.GetComponent<Transform>();
    }

    void Update()
    {

        //Vector2 wantedVelocity = GetInput() * sensitivity;
        //wantedVelocity = wantedVelocity * Time.deltaTime;

        //transform.localEulerAngles = new Vector3(0, wantedVelocity.y, 0);
        //cameraTransform.localEulerAngles = new Vector3(wantedVelocity.x, 0, 0);



        //var pitch = 0f;
        //var yaw = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivity;

        //if (!invertCamera)
        //{
        //    pitch -= sensitivity * Input.GetAxis("Mouse Y");
        //}
        //else
        //{
        //    // Inverted Y
        //    pitch += sensitivity * Input.GetAxis("Mouse Y");
        //}

        //// Clamp pitch between lookAngle
        //pitch = Mathf.Clamp(pitch, -maxLookAngle, maxLookAngle);

        //transform.localEulerAngles = new Vector3(0, yaw, 0);
        //cameraTransform.localEulerAngles = new Vector3(pitch, 0, 0);



        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = -Input.GetAxis("Mouse Y");

        //// transform.RotateAround(transform.transform.position, -Vector3.up, rotateHorizontal * sensitivity);
        //// transform.Rotate(transform.transform.position, -Vector3.up, rotateHorizontal * sensitivity);
        transform.Rotate(0f, rotateHorizontal * sensitivity, 0f);
        cameraTransform.Rotate(rotateVertical * sensitivity, 0f, 0f);
        //// transform.RotateAround(Vector3.zero, transform.right, rotateVertical * sensitivity);


        //Quaternion rotation = cameraTransform.rotation * Quaternion.Euler(rotateVertical * sensitivity, 0f, 0f);
        //cameraTransform.rotation = Quaternion.Slerp(cameraTransform.rotation, rotation, Time.deltaTime);
    }
}
