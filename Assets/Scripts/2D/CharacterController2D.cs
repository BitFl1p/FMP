using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController2D : MonoBehaviour
{
    public Transform lockPos;
    public bool flip;
    public float speed, jumpPow;
    [HideInInspector] public bool isGrounded;
    public string axis = "XY";
    Rigidbody rb;
    Animator anim;
    [HideInInspector] public float lastMove;
    public bool clampDisabled;
    public float knockCount;
    InputMaster input;
    public float drag;

    private void Awake()
    {
        input = new InputMaster();
    }
    private void OnEnable()
    {
        input.Enable();
        if (!flip)
        {
            if (axis == "ZY") transform.eulerAngles = new Vector3(180, -90, -180);
            else transform.eulerAngles = new Vector3(180, 180, -180);
        }
        else
        {
            if (axis == "ZY") transform.eulerAngles = new Vector3(180, 90, -180);
            else transform.eulerAngles = new Vector3(180, 0, -180);
        }
    }
    private void OnDisable()
    {
        input.Disable();
    }
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

    }
    void Update()
    {
        speed = GetComponent<Stats>().moveSpeed;
        if (axis == "XY")
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, lockPos.position.z);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;

        }
        else
        {
            transform.position = new Vector3(lockPos.position.x, transform.position.y, transform.position.z);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        }
        Vector2 moveInput = input.Player2D.Move.ReadValue<Vector2>();
        if (moveInput != Vector2.zero)
        {
            if (axis == "XY")
            {
                if (clampDisabled)
                {
                    knockCount -= Time.deltaTime;
                    if (knockCount <= 0)
                    {
                        clampDisabled = false;
                        knockCount = 0.25f;
                    }

                }
                else
                {
                    if (flip)
                    {
                        rb.velocity = new Vector3(rb.velocity.x + (input.Player2D.Move.ReadValue<Vector2>().x * speed), rb.velocity.y, lockPos.position.z);
                    }
                    else
                    {
                        rb.velocity = new Vector3(rb.velocity.x + (-1 * input.Player2D.Move.ReadValue<Vector2>().x * speed), rb.velocity.y, lockPos.position.z);
                    }

                }

                rb.velocity = new Vector3(Mathf.Clamp(GoTowardsZero(rb.velocity.x, drag), speed * -10, speed * 10), rb.velocity.y, lockPos.position.z);
                
            }
                
                    

            if (axis == "ZY")
            {
                if (clampDisabled)
                {
                    knockCount -= Time.deltaTime;
                    if (knockCount <= 0)
                    {
                        clampDisabled = false;
                        knockCount = 1;
                    }
                    return;
                }
                else
                {
                    if (flip)
                    {
                        rb.velocity = new Vector3(lockPos.position.x, rb.velocity.y, rb.velocity.z + (-1 * input.Player2D.Move.ReadValue<Vector2>().x * speed));
                    }
                    else
                    {
                        rb.velocity = new Vector3(lockPos.position.x, rb.velocity.y, rb.velocity.z + (input.Player2D.Move.ReadValue<Vector2>().x * speed));
                    }
                }
                
                
                rb.velocity = new Vector3(lockPos.position.x, rb.velocity.y, Mathf.Clamp(GoTowardsZero(rb.velocity.z, drag), speed * -4, speed * 4));
            }
                
                    
            lastMove = input.Player2D.Move.ReadValue<Vector2>().x;
        }
        if (!clampDisabled)
        {
            if (axis == "XY")
            {
                rb.velocity = new Vector3(Mathf.Clamp(GoTowardsZero(rb.velocity.x, drag), speed * -4, speed * 4), rb.velocity.y, lockPos.position.z);
            }
            if (axis == "ZY")
            {
                rb.velocity = new Vector3(lockPos.position.x, rb.velocity.y, Mathf.Clamp(GoTowardsZero(rb.velocity.z, drag), speed * -6, speed * 6));
            }
        }
        

        if (isGrounded && InputSystem.GetDevice<Keyboard>().spaceKey.wasPressedThisFrame) { rb.velocity = new Vector3(rb.velocity.x, jumpPow, rb.velocity.z); }
        if (input.Player2D.Move.ReadValue<Vector2>().x != 0) anim.SetBool("Moving", true); else anim.SetBool("Moving", false);
        if (!flip)
        {
            if (axis == "ZY") if (lastMove < 0) transform.eulerAngles = new Vector3(180, -90, -180); else transform.eulerAngles = new Vector3(180, 90, -180);
            else if (lastMove < 0) transform.eulerAngles = new Vector3(180, 180, -180); else transform.eulerAngles = new Vector3(180, 0, -180);
        }
        else
        {
            if (axis == "ZY") if (lastMove < 0) transform.eulerAngles = new Vector3(180, 90, -180); else transform.eulerAngles = new Vector3(180, -90, -180);
            else if (lastMove < 0) transform.eulerAngles = new Vector3(180, 0, -180); else transform.eulerAngles = new Vector3(180, 180, -180);
        }
        
        anim.SetBool("Jump", !isGrounded);

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
}
