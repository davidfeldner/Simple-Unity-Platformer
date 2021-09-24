using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 12f;

    Vector3 velocity ;
    public float gravity = -9.81f;
    public float jumpheight = 3f;

    public Transform groundCheck;
    public float groundDistanse = 0.1f;
    public LayerMask groundMask;
    bool isGrounded;

    public float dashSpeed = 1f;
    public float dashTimeLength = 0.1f;
    public float dashCoolDown = 1f;

    private float dashTimer = 0f;
    private float dashCoolDownTimer = 0f;
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistanse, groundMask);
        
        if (isGrounded && velocity.y < 0) {
            velocity.y = -2f;

        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if (Input.GetButtonDown("Jump") && isGrounded) {
            velocity.y = Mathf.Sqrt(jumpheight * -2f * gravity);
        }

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);


        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCoolDownTimer <= Time.time) {
            dashCoolDownTimer = Time.time + dashCoolDown / 100;
            dashTimer = Time.time + dashTimeLength;
            
        }

        if (Time.time < dashTimer) {
            dashTimer -= Time.deltaTime;
            dash();
            Debug.Log("calls");
        }
    }
    void dash() {
        controller.Move(transform.forward * dashSpeed*50 * Time.deltaTime);
    }

}
