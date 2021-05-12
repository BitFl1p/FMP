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
    float lastMove;
    public bool clampDisabled;
    public float knockCount;
    InputMaster input;
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
                if (flip && !clampDisabled)
                {
                    rb.velocity = new Vector3(rb.velocity.x + (input.Player2D.Move.ReadValue<Vector2>().x * speed), rb.velocity.y, 0);
                }
                else if(!clampDisabled)
                {
                    rb.velocity = new Vector3(rb.velocity.x + (-1 * input.Player2D.Move.ReadValue<Vector2>().x * speed), rb.velocity.y, 0);
                }
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
                    rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, speed * -5, speed * 5), rb.velocity.y, 0);
                }
            }
                
                    

            if (axis == "ZY")
            {
                if (flip && !clampDisabled)
                {
                    rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z + (-1 * input.Player2D.Move.ReadValue<Vector2>().x * speed));
                }

                else if (!clampDisabled)
                {
                    rb.velocity = new Vector3(0, rb.velocity.y, rb.velocity.z + (input.Player2D.Move.ReadValue<Vector2>().x * speed));
                }
                if (clampDisabled)
                {
                    knockCount -= Time.deltaTime;
                    if (knockCount <= 0)
                    {
                        clampDisabled = false;
                        knockCount = 1;
                    }
                }
                else
                {
                    rb.velocity = new Vector3(0, rb.velocity.y, Mathf.Clamp(rb.velocity.z, speed * -5, speed * 5));
                }
            }
                
                    
            lastMove = input.Player2D.Move.ReadValue<Vector2>().x;
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
}
