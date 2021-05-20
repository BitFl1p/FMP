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
    public bool isGrounded, walled, flip;
    bool start;
    internal virtual void OnEnable()
    {
        start = true;
        SetRB();
        InvokeRepeating("UpdatePath", 0f, .5f);
        InvokeRepeating("UpdateTarget", 0f, 2f);
        if (target == null) target = FindObjectOfType<CharacterController2D>()?.transform; if (target == null) return;
    }
    internal virtual void FixedUpdate()
    {
        if (target == null || !target.gameObject.activeInHierarchy)
        {
            if (axis == "XY")
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 90, 0);
            }
        }
        PlayerSeen();
        Attack();
    }
    internal virtual void UpdatePath()
    {
        if (start)
        {
            if (axis == "XY")
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
                transform.eulerAngles = new Vector3(0, 90, 0);
            }
            
            start = false;
        }
        rb.angularVelocity = Vector3.zero;
        if (target == null) return;
        else PlayerSeen();
    }
    internal virtual void Attack()
    {
        if (target == null) if (FindObjectOfType<CharacterController2D>() != null) target = FindObjectOfType<CharacterController2D>().transform; if (target == null) return;
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
    internal virtual void UpdateTarget()
    {
        if (target == null) target = FindObjectOfType<CharacterController2D>()?.transform; if (target == null) return;
        if (target.gameObject.activeInHierarchy == false) target = null;
    }
    internal virtual void PlayerSeen()
    {
        if (target == null) return;
        
        Vector3 targetDir = Quaternion.LookRotation((target.position - transform.position).normalized, Vector3.up).eulerAngles;
        Debug.Log(new Vector3((target.position.x - transform.position.x), 0, 0).normalized.x);
        Debug.Log(new Vector3(0, 0, (target.position.z - transform.position.z)).normalized.z);
        if (target.gameObject.activeInHierarchy)
        {
            if (axis == "ZY")
            {
                if (!flip)
                {
                    if (new Vector3(0, 0, (target.position.z - transform.position.z)).normalized.z > 0) transform.eulerAngles = new Vector3(0, 90, 0);
                    else transform.eulerAngles = new Vector3(0, 270, 0);
                }
                else
                {
                    if (new Vector3(0, 0, (target.position.z - transform.position.z)).normalized.z > 0) transform.eulerAngles = new Vector3(0, 270, 0);
                    else transform.eulerAngles = new Vector3(0, 90, 0);
                }
                rb.velocity += new Vector3(0, 0, (target.position.z - transform.position.z)).normalized * speed;
                rb.velocity = new Vector3(0, rb.velocity.y, Mathf.Clamp(rb.velocity.z, speed * -10, speed * 10));
            }
            else
            {
                if (!flip)
                {
                    if (new Vector3((target.position.x - transform.position.x), 0, 0).normalized.x > 0) transform.eulerAngles = new Vector3(0, 0, 0);
                    else transform.eulerAngles = new Vector3(0, 180, 0);
                }
                else
                {
                    if (new Vector3((target.position.x - transform.position.x), 0, 0).normalized.x > 0) transform.eulerAngles = new Vector3(0, 180, 0);
                    else transform.eulerAngles = new Vector3(0, 0, 0);
                }

                rb.velocity += new Vector3((target.position.x - transform.position.x), 0, 0).normalized * speed;
                rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, speed * -10, speed * 10), rb.velocity.y, 0);

            }
            if(walled && isGrounded)
            {
                rb.velocity += new Vector3(0, speed*5, 0);
                isGrounded = false;
            }

        }
        else
        {
            if (axis == "XY") transform.eulerAngles = new Vector3(0, 0, 0);
            else transform.eulerAngles = new Vector3(0, 90, 0);
        }
    }
    internal virtual void SetRB()
    {
        rb = GetComponent<Rigidbody>();
    }
    public int moneyToDrop;
    internal virtual void OnDestroy()
    {
        if(target != null) target.GetComponent<Stats>().Coins2D += moneyToDrop;
    }
}
