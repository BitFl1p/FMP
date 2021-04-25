using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainCaseAI : EnemyAI
{
    float count = 7;
    internal override void FixedUpdate()
    {
        base.FixedUpdate();
        transform.LookAt(target);
    }
    internal override void Attack()
    {
        if(count <= 0)
        {
            anim.SetBool("Attacking", true);
            count = 7;
        }
        else
        {
            anim.SetBool("Attacking", false);
            count -= Time.deltaTime;
        }
        
    }
    internal override void PlayerSeen()
    {
        if (path == null)
        {
            return;
        }
        if (Vector3.Distance(rb.position, target.position) < targetDist)
        {
            direction = (target.position - rb.position).normalized;
            direction = -direction;
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
            
            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, speed * -10, speed * 10), Mathf.Clamp(rb.velocity.y, speed * -10, speed * 10), Mathf.Clamp(rb.velocity.z, speed * -10, speed * 10));

            if (Vector3.Distance(new Vector3(rb.position.x, 0, rb.position.z), path.vectorPath[currentWaypoint]) < nextWaypointDistance)
            {
                currentWaypoint++;
            }
            reachedEndOfPath = false;
        }
    }
}
