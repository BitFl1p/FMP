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
    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    Seeker seeker;
    public Rigidbody rb;
    bool leftLast; bool rightLast;
    Vector3 direction, force;
    // Start is called before the first frame update
    void Start()
    {
        target = FindObjectOfType<ThirdPersonMovement>().GetComponent<UnityEngine.Transform>();
        seeker = GetComponent<Seeker>();

        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    void UpdatePath()
    {
        if (seeker.IsDone()) seeker.StartPath(rb.position, target.transform.position, OnPathComplete);
        PlayerSeen();
    }
    // Update is called once per frame

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    public void PlayerSeen()
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
        rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x + (path.vectorPath[currentWaypoint].x - rb.position.x) * speed, -speed * 10, speed * 10), rb.velocity.y, Mathf.Clamp(rb.velocity.z + (path.vectorPath[currentWaypoint].z - rb.position.z) * speed, -speed * 10, speed * 10));

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }
    }
}
