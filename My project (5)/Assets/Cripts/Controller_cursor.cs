using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_cursor : MonoBehaviour
{
    public float  mouseSensitiviti = 900f;
    public Transform playerBody;
    float xRotation = 0f;
    public bool IsFreeze =  false;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        
    }
   

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            IsFreeze = !IsFreeze;
            if (IsFreeze == true)
            {
                mouseSensitiviti = 0f;
            }
            else
            {
                mouseSensitiviti = 900f;
            }

        }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitiviti * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitiviti * Time.deltaTime;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}