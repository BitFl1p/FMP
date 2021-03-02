using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
    public bool flip;
    public float speed, jumpPow;
    [HideInInspector] public bool isGrounded;
    public string axis = "XY";
    static Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if(axis == "XY") { if (flip) { rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x + (-1 * Input.GetAxisRaw("Horizontal") * speed), speed * -5, speed * 5), rb.velocity.y, 0); } else { rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x + (Input.GetAxisRaw("Horizontal") * speed), speed * -5, speed * 5), rb.velocity.y, 0); } }

        if (axis == "ZY") { if (flip) { rb.velocity = new Vector3(0, rb.velocity.y, Mathf.Clamp(rb.velocity.z + (-1 * Input.GetAxisRaw("Horizontal") * speed), speed * -5, speed * 5)); } else { rb.velocity = new Vector3(0, rb.velocity.y, Mathf.Clamp(rb.velocity.z + (Input.GetAxisRaw("Horizontal") * speed), speed * -5, speed * 5)); } }
        
        if (isGrounded && Input.GetKey(KeyCode.Space)) { rb.velocity = new Vector3(rb.velocity.x, jumpPow, rb.velocity.z); }
    }
}
