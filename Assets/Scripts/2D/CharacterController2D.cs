using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    
    public bool flip;
    public float speed, jumpPow, lockPos;
    [HideInInspector] public bool isGrounded;
    public string axis = "XY";
    Rigidbody rb;
    Animator anim;
    float lastMove;
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        
    }
    void Update()
    {
        if (axis == "XY") transform.position = new Vector3(transform.position.x,transform.position.y, lockPos);
        else transform.position = new Vector3(lockPos, transform.position.y, transform.position.z);
        Vector2 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        if (moveInput != Vector2.zero)
        {
            if (axis == "XY") if (flip) rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x + (-1 * Input.GetAxisRaw("Horizontal") * speed), speed * -5, speed * 5), rb.velocity.y, 0); else rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x + (Input.GetAxisRaw("Horizontal") * speed), speed * -5, speed * 5), rb.velocity.y, 0);

            if (axis == "ZY") if (flip) rb.velocity = new Vector3(0, rb.velocity.y, Mathf.Clamp(rb.velocity.z + (-1 * Input.GetAxisRaw("Horizontal") * speed), speed * -5, speed * 5)); else rb.velocity = new Vector3(0, rb.velocity.y, Mathf.Clamp(rb.velocity.z + (Input.GetAxisRaw("Horizontal") * speed), speed * -5, speed * 5));
            lastMove = Input.GetAxisRaw("Horizontal");
        }

        if (isGrounded && Input.GetKey(KeyCode.Space)) { rb.velocity = new Vector3(rb.velocity.x, jumpPow, rb.velocity.z); }
        if (Input.GetAxisRaw("Horizontal") != 0) anim.SetBool("Moving", true); else anim.SetBool("Moving", false);
        if (lastMove < 0) transform.eulerAngles = new Vector3(180, 90, -180); else transform.eulerAngles = new Vector3(180, -90, -180);
        anim.SetBool("Jump", !isGrounded);

    }
}
