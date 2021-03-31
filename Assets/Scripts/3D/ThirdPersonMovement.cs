using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class ThirdPersonMovement : MonoBehaviour
{
    Rigidbody rb;
    public float offset = 50, drag = 0.5f;
    public CinemachineFreeLook vcam;
    Vector3 velocity, moveDir = Vector3.zero;
    public float gravity = -9.81f, speed = 6, turnSmoothTime = 0.1f, jumpHeight = 10;
    public Transform cam;
    //public CharacterController controller;
    float turnSmoothVelocity, horizontal, vertical, jumpCount;
    public bool grounded;
    Animator anim;
    public Transform hip;
    public int jumpAmount;
    bool jumped;
    private void Start()
    {
        jumpAmount = GetComponent<Stats>().jumpAmount;
        anim = GetComponentInChildren<Animator>();
        rb = GetComponentInChildren<Rigidbody>();
    }
    private void FixedUpdate()
    {
        anim.SetBool("Grounded", grounded);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Jump();
        Move();
        Aim();
    }

    void Aim()
    {
        hip.localEulerAngles = new Vector3(0, 180, vcam.m_YAxis.Value * 90 - offset);
        /*Quaternion rot = hip.localRotation;
        rot.eulerAngles = new Vector3(0.0f, vcam.m_YAxis.Value * 180, 0.0f);
        hip.localRotation = rot;*/
    }
    float GoTowardsZero(float value, float speed)
    {
        if (value > speed)
        {
            value -= speed;
        } else if(value < -speed)
        {
            value += speed;
        } else
        {
            value = 0;
        }
        return value;
    }
    void Jump()
    {
        
        if (jumpCount <= 0)
        {
            if (grounded)
            {

                if (Input.GetButtonDown("Jump"))
                {
                    rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt((jumpHeight * GetComponent<Stats>().jumpHeight) * -2f * gravity), rb.velocity.z);
                    jumpAmount = GetComponent<Stats>().jumpAmount - 1;
                    grounded = false;
                    jumpCount = 0.2f;
                }

            }
            else if (jumpAmount >= 1)
            {
                if (Input.GetButtonDown("Jump"))
                {
                    rb.velocity = new Vector3(rb.velocity.x, Mathf.Sqrt((jumpHeight * GetComponent<Stats>().jumpHeight) * -2f * gravity), rb.velocity.z);
                    jumpAmount--;
                    jumpCount = 0.2f;
                }
            }
        }
        if (jumpCount > 0)
        {
            jumpCount -= Time.deltaTime;
        }
        
    }
    void Move()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        if (direction.magnitude >= 0.1f)
        {

            moveDir = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward);

            rb.velocity = new Vector3(moveDir.x * (speed * GetComponent<Stats>().moveSpeed), rb.velocity.y, moveDir.z * (speed * GetComponent<Stats>().moveSpeed));
            anim.SetBool("Schmove", true);

        }
        else
        {

            anim.SetBool("Schmove", false);
        }
        rb.velocity = new Vector3(GoTowardsZero(rb.velocity.x, drag), rb.velocity.y, GoTowardsZero(rb.velocity.z, drag));
        anim.SetFloat("SpeedX", horizontal, Time.deltaTime * 2f, Time.deltaTime);
        anim.SetFloat("SpeedY", vertical, Time.deltaTime * 2f, Time.deltaTime);
    }

}
