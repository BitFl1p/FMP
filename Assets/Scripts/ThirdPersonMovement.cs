using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ThirdPersonMovement : MonoBehaviour
{
    public float offset = 50;
    public CinemachineFreeLook vcam;
    Vector3 velocity, moveDir = Vector3.zero;
    public float gravity = -9.81f, speed = 6, turnSmoothTime = 0.1f, jumpHeight = 10;
    public Transform cam;
    public CharacterController controller;
    float turnSmoothVelocity, horizontal, vertical;
    public bool grounded;
    Animator anim;
    public Transform hip;

    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    
    private void FixedUpdate()
    {
        anim.SetBool("Grounded", grounded);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        //you do not understand the GRAVITY of the situation
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if (grounded)
        {
            velocity.y = 0;
            if (Input.GetButtonDown("Jump"))
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                grounded = false;
            }

        }
        //schmovement
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        if (direction.magnitude >= 0.1f)
        {

            moveDir = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward);

            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            anim.SetBool("Schmove", true);

        }
        else
        {
            anim.SetBool("Schmove", false);
        }
        
        anim.SetFloat("SpeedX", horizontal, Time.deltaTime * 2f, Time.deltaTime);
        anim.SetFloat("SpeedY", vertical, Time.deltaTime * 2f, Time.deltaTime);
        Aim();
    }
    void Aim()
    {
        hip.localEulerAngles = new Vector3(0, 180, vcam.m_YAxis.Value * 90 - offset);
        /*Quaternion rot = hip.localRotation;
        rot.eulerAngles = new Vector3(0.0f, vcam.m_YAxis.Value * 180, 0.0f);
        hip.localRotation = rot;*/
    }

}
