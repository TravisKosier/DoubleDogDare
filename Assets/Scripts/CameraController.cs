using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    
    public float mouseSensitivity = 100f;

    public Transform playerBody;

    float xRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX;
        float mouseY;
        if ((Mathf.Abs(Input.GetAxis("JoystickX")) > 0.1)) //Sensing controller input
        {
            mouseX = Input.GetAxis("JoystickX") * mouseSensitivity * Time.deltaTime;
        }
        else //Default M+KB
        {
            mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        }
        if ((Mathf.Abs(Input.GetAxis("JoystickY")) > 0.1)) //Sensing controller input
        {
            mouseY = Input.GetAxis("JoystickY") * mouseSensitivity * Time.deltaTime;
        }
        else //Default M+KB
        {
            mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        }
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
