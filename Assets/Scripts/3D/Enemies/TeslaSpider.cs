using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeslaSpider : EnemyAI
{
    bool attacking;
    internal override void FixedUpdate()
    {
        if (!attacking) PlayerSeen();
        Attack();
        transform.LookAt(transform.position+rb.velocity.normalized);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
    internal override void Attack()
    {

        if (Vector3.Distance(rb.position, target.position) < targetDist)
        {
            rb.isKinematic = true;
            rb.velocity = Vector3.zero;
            anim.SetBool("Attacking", true);
            attacking = true;
        }
        else if(!attacking)
        {
            foreach (GameObject hurtbox in hurtboxes) { hurtbox.SetActive(false); }
            anim.SetBool("Attacking", false);
            
        }
    }
    
}
