using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(EnemyHealth2D))]
public class EnemyAI2D : MonoBehaviour
{
    public string axis = "XY";
    public float knockback;
    public Animator anim;
    public int damage;
    public GameObject[] hurtboxes;
    public Transform target;
    public float speed = 5f, targetDist = 100;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public bool isGrounded, walled;
    internal virtual void OnEnable()
    {
        if (axis == "XY") GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        else GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        SetRB();
        if (target == null) if (FindObjectOfType<CharacterController2D>() != null) target = FindObjectOfType<CharacterController2D>().gameObject.transform;
        InvokeRepeating("UpdatePath", 0f, .5f);
        if (axis == "XY") transform.eulerAngles = new Vector3(0, 0, 0);
        else transform.eulerAngles = new Vector3(0, 90, 0);
    }
    internal virtual void FixedUpdate()
    {
        PlayerSeen();
        Attack();
    }
    internal virtual void UpdatePath()
    {
        if (target == null) {if (FindObjectOfType<CharacterController2D>() != null) { target = FindObjectOfType<CharacterController2D>().gameObject.transform; } }
        else PlayerSeen();
    }
    internal virtual void Attack()
    {
        if (Vector3.Distance(rb.position, target.position) < targetDist)
        {
            foreach (GameObject hurtbox in hurtboxes) { hurtbox.GetComponent<DealDamage>().damage = damage; hurtbox.GetComponent<DealDamage>().knockback = knockback; }
            anim.SetBool("Attacking", true);
        }
        else
        {
            foreach (GameObject hurtbox in hurtboxes) { hurtbox.SetActive(false); }
            anim.SetBool("Attacking", false);
        }
    }

    internal virtual void PlayerSeen()
    {
        if (target == null) return;
        Vector3 targetDir = Quaternion.LookRotation((target.position - transform.position).normalized, Vector3.up).eulerAngles;
        if (target.gameObject.activeInHierarchy)
        {
            if (axis == "ZY")
            {
                if (targetDir.y >= 135 && targetDir.y <= 225)
                {
                    rb.velocity -= new Vector3(0, 0, 1) * speed;
                    transform.eulerAngles = new Vector3(0,90,0);
                }
                else
                {
                    rb.velocity += new Vector3(0, 0, 1) * speed;
                    transform.eulerAngles = new Vector3(0, -90, 0);
                }

                rb.velocity = new Vector3(0, rb.velocity.y, Mathf.Clamp(rb.velocity.z, speed * -10, speed * 10));
            }
            else
            {
                if (targetDir.y >= 45 && targetDir.y <= 135)
                {
                    rb.velocity -= new Vector3(1, 0, 0) * speed;
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
                else
                {
                    rb.velocity += new Vector3(1, 0, 0) * speed;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }

                rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, speed * -10, speed * 10), rb.velocity.y, 0);

            }
            if(walled && isGrounded)
            {
                rb.velocity += new Vector3(0, speed*5, 0);
            }
            
        }
    }
    internal virtual void SetRB()
    {
        rb = GetComponent<Rigidbody>();
    }
}
