using Cinemachine;
using UnityEngine;
public class ThirdPersonMovement : MonoBehaviour
{
    Rigidbody rb;
    public float offset = 50, drag = 0.5f;
    public CinemachineFreeLook vcam;
    Vector3 velocity, moveDir = Vector3.zero;
    public float gravity = -9.81f, speed, turnSmoothTime = 0.1f, jumpHeight;
    public Transform cam;
    //public CharacterController controller;
    float turnSmoothVelocity, horizontal, vertical, jumpCount;
    public bool grounded;
    Animator anim;
    public Transform hip;
    public int jumpAmount;
    bool jumped;
    float dashCount = 3;
    public GameObject trail;
    private void Start()
    {
        jumpHeight = GetComponent<Stats>().jumpHeight;
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
        hip.localEulerAngles = new Vector3(0, 180, vcam.m_YAxis.Value * 180 - offset);
    }
    float GoTowardsZero(float value, float speed)
    {
        if (value > speed)
        {
            value -= speed;
        }
        else if (value < -speed)
        {
            value += speed;
        }
        else
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
                jumpHeight = GetComponent<Stats>().jumpHeight;
                jumpAmount = GetComponent<Stats>().jumpAmount;

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
        speed = GetComponent<Stats>().moveSpeed;
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, cam.eulerAngles.y, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        if (direction.magnitude >= 0.1f)
        {

            moveDir = (Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward);
            if(dashCount <= 0 && Input.GetKey(KeyCode.LeftShift))
            {
                rb.velocity = new Vector3(moveDir.x * (speed * GetComponent<Stats>().moveSpeed) * 10, rb.velocity.y, moveDir.z * (speed * GetComponent<Stats>().moveSpeed) * 10);
                dashCount = 3;
            }
            else
            {
                rb.velocity = new Vector3(moveDir.x * (speed * GetComponent<Stats>().moveSpeed), rb.velocity.y, moveDir.z * (speed * GetComponent<Stats>().moveSpeed));
            }
            anim.SetBool("Schmove", true);

        }
        else
        {

            anim.SetBool("Schmove", false);
        }
        if (dashCount > 0) dashCount -= Time.deltaTime;
        if (dashCount < 2.8)
        {
            rb.velocity = new Vector3(GoTowardsZero(rb.velocity.x, System.Math.Abs(rb.velocity.x / drag)), rb.velocity.y, GoTowardsZero(rb.velocity.z, System.Math.Abs(rb.velocity.z / drag)));
            trail.SetActive(false);
        }
        else
        {
            rb.velocity = new Vector3(moveDir.x * (speed * GetComponent<Stats>().moveSpeed) * 10, rb.velocity.y, moveDir.z * (speed * GetComponent<Stats>().moveSpeed) * 10);
            trail.SetActive(true);
        }
        anim.SetFloat("SpeedX", horizontal, Time.deltaTime * 2f, Time.deltaTime);
        anim.SetFloat("SpeedY", vertical, Time.deltaTime * 2f, Time.deltaTime);
    }

}
