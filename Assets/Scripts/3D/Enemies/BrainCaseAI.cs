using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainCaseAI : EnemyAI
{
    float count;
    internal override void OnEnable()
    {
        base.OnEnable();
        count = Random.Range(3, 4);
    }
    internal override void FixedUpdate()
    {
        base.FixedUpdate();
        transform.LookAt(target);
    }
    internal override void UpdatePath()
    {
        if (target == null) if (FindObjectOfType<ThirdPersonMovement>() != null) target = FindObjectOfType<ThirdPersonMovement>().GetComponent<Transform>();
        if (seeker.IsDone() && target != null) path = seeker.StartPath(new Vector3(rb.position.x, target.position.y, rb.position.y), target.transform.position);
    }
    internal override void Attack()
    {
        if(count <= 0)
        {
            anim.SetBool("Attacking", true);
            count = Random.Range(3, 4);
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
        if (Vector3.Distance(new Vector3(rb.position.x, target.position.y, rb.position.y), target.position) < targetDist)
        {
            direction = (target.position - rb.position).normalized;
            direction = -direction;
            if (rb.position.y > target.position.y + 20)
            {
                direction.y = -1;
            }
            else if (rb.position.y < target.position.y)
            {
                direction.y = 1;
            }
            else
            {
                direction.y = Random.Range(0, 1);
            }
            rb.velocity += direction * speed;
            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, speed * -10, speed * 10), Mathf.Clamp(rb.velocity.y, speed * -10, speed * 10), Mathf.Clamp(rb.velocity.z, speed * -10, speed * 10));
            currentWaypoint = 0;
            reachedEndOfPath = true;
            return;
        }
        else reachedEndOfPath = false;
        if (currentWaypoint >= path.vectorPath.Count)
        {
            currentWaypoint = 0;
            reachedEndOfPath = true;
            return;
        }
        else reachedEndOfPath = false;
        if(!reachedEndOfPath)
        {
            direction = (path.vectorPath[currentWaypoint] - rb.position).normalized;
            if (rb.position.y > target.position.y + 20)
            {
                direction.y = -1;
            }
            else if (rb.position.y < target.position.y)
            {
                direction.y = 1;
            }
            else
            {
                direction.y = Random.Range(0, 1);
            }
            rb.velocity += direction * speed;
            rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, speed * -10, speed * 10), Mathf.Clamp(rb.velocity.y, speed * -10, speed * 10), Mathf.Clamp(rb.velocity.z, speed * -10, speed * 10));
            if (Vector3.Distance(new Vector3(rb.position.x, 0, rb.position.z), path.vectorPath[currentWaypoint]) < nextWaypointDistance)
            {
                currentWaypoint++;
            }
            reachedEndOfPath = false;
        }
        if(reachedEndOfPath && Vector3.Distance(new Vector3(rb.position.x, target.position.y, rb.position.y), target.position) > targetDist)
        {
            reachedEndOfPath = false;
        }
    }
}
