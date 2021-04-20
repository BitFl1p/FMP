using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyAI : MonoBehaviour
{
    public Transform target;
    public float speed = 200f;
    public float nextWaypointDistance = 3f;
    public bool slidy;
    internal Path path;
    internal int currentWaypoint = 0;
    internal bool reachedEndOfPath = false;
    internal Seeker seeker;
    internal float distance;
    public Rigidbody rb;
    internal bool leftLast; bool rightLast;
    internal Vector3 direction = Vector3.zero;
    // Start is called before the first frame update
    internal virtual void OnEnable()
    {
        target = FindObjectOfType<ThirdPersonMovement>().GetComponent<Transform>();
        seeker = GetComponent<Seeker>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }
    internal virtual void FixedUpdate()
    {
        PlayerSeen();
        Attack();
    }
    internal virtual void UpdatePath()
    {
        if (seeker.IsDone()) path = seeker.StartPath(rb.position, target.transform.position);
    }
    internal virtual void Attack()
    {
        if (reachedEndOfPath) GetComponentInChildren<Animator>().SetBool("Attacking", true); 
        else GetComponentInChildren<Animator>().SetBool("Attacking", false);
    }

    internal virtual void PlayerSeen()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }
        //rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x + (path.vectorPath[currentWaypoint].x - rb.position.x) * speed, -speed * 10, speed * 10), rb.velocity.y, Mathf.Clamp(rb.velocity.z + (path.vectorPath[currentWaypoint].z - rb.position.z) * speed, -speed * 10, speed * 10));
        direction = (path.vectorPath[currentWaypoint]-rb.position).normalized;
        direction.y = 0;
        //Debug.DrawRay(rb.position, direction*100, Color.red, .5f);
        rb.velocity += direction * speed;
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, speed * -10, speed * 10), rb.velocity.y, Mathf.Clamp(rb.velocity.z, speed * -10, speed * 10));

        distance = Vector3.Distance(rb.position, path.vectorPath[currentWaypoint]);
        Debug.Log(distance);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
