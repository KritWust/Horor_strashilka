using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerJump : MonoBehaviour
{
    public float jumpForce = 9f;
    public float gravity = 30f;


    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (controller.isGrounded)
        {
            moveDirection.y = 0f;

            if (Input.GetKey(KeyCode.Space))
            {
                moveDirection.y = jumpForce;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}