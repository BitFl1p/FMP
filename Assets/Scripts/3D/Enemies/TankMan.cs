using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankMan : EnemyAI
{
    internal override void FixedUpdate()
    {
        base.FixedUpdate();
        if (target == null) return;
        transform.LookAt(target); transform.eulerAngles += new Vector3(0, 90, 0);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
    internal override void PlayerSeen()
    {
        if (path == null)
        {
            return;
        }
        if (Vector3.Distance(rb.position, target.position) < targetDist)
        {
            
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

            if (Vector3.Distance(new Vector3(rb.position.x, 0, rb.position.z), path.vectorPath[currentWaypoint]) < nextWaypointDistance)
            {
                currentWaypoint++;
            }
            reachedEndOfPath = false;
        }
    }
}
