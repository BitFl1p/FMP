using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
[RequireComponent(typeof(Seeker)), RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(EnemyHealth))]
public class EnemyAI : MonoBehaviour
{
    public float knockback;
    public Animator anim;
    public int damage;
    public GameObject[] hurtboxes;
    public Transform target;
    public float speed = 5f, targetDist = 100;
    public float nextWaypointDistance = 10f;
    internal Path path;
    internal int currentWaypoint = 0;
    internal bool reachedEndOfPath = false;
    internal Seeker seeker;
    [HideInInspector]public Rigidbody rb;
    internal Vector3 direction = Vector3.zero;

    // Start is called before the first frame update
    internal virtual void OnEnable()
    {
        anim.SetBool("Attacking", false);
        if(FindObjectOfType<ThirdPersonMovement>() != null) target = FindObjectOfType<ThirdPersonMovement>().gameObject.transform;
        seeker = GetComponent<Seeker>();
        InvokeRepeating("UpdatePath", 0f, .5f); 
        InvokeRepeating("UpdateTarget", 0f, 2f);
    }
    internal virtual void UpdateTarget()
    {
        if(target == null) target = FindObjectOfType<ThirdPersonMovement>()?.gameObject.transform; if (target == null) return;
        if (!target.gameObject.activeInHierarchy) target = null; 
    }
    internal virtual void FixedUpdate()
    {
        if(target!=null)PlayerSeen();
        Attack();
    }
    internal virtual void UpdatePath()
    {
        if (target == null) return;
        if (seeker.IsDone() && target != null) path = seeker.StartPath(rb.position, target.transform.position);
    }
    internal virtual void Attack()
    {

        if (reachedEndOfPath)
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
        if (path == null)
        {
            return;
        }
        if (Vector3.Distance(rb.position, target.position) < targetDist)
        {
            direction = (target.position - rb.position).normalized;
            direction.y = 0;
            rb.velocity += direction * speed;
            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, speed * -10, speed * 10), rb.velocity.y, Mathf.Clamp(rb.velocity.z, speed * -10, speed * 10));
            currentWaypoint = 0;
            reachedEndOfPath = true;
            return;
        }
        else if (currentWaypoint >= path.vectorPath.Count)
        {
            currentWaypoint = 0;
            reachedEndOfPath = true;
            return;
        }
        else
        {
            direction = (path.vectorPath[currentWaypoint] - rb.position).normalized;
            direction.y = 0;
            rb.velocity += direction * speed;
            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, speed * -10, speed * 10), rb.velocity.y, Mathf.Clamp(rb.velocity.z, speed * -10, speed * 10));

            if (Vector3.Distance(new Vector3(rb.position.x,0,rb.position.z), path.vectorPath[currentWaypoint]) < nextWaypointDistance)
            {
                currentWaypoint++;
            }
            reachedEndOfPath = false;
        }
    }
    internal virtual void SetRB()
    {
        rb = GetComponent<Rigidbody>();
    }
    public int moneyToDrop;
    internal virtual void OnDestroy()
    {
        if(target) target.GetComponent<Stats>().Coins3D += moneyToDrop;
    }
}
