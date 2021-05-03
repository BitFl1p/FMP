using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechElf : EnemyAI
{
    bool attacking;
    internal override void FixedUpdate()
    {
        if(!attacking) PlayerSeen();
        Attack();
        transform.LookAt(rb.velocity.normalized);
        if (!attacking) { transform.LookAt(transform.position + rb.velocity); transform.eulerAngles += new Vector3(0, 90, 0); transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0); }
        else { transform.LookAt(target.position); transform.eulerAngles += new Vector3(0, 90, 0); transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0); };
    }
    internal override void Attack()
    {

        if (Vector3.Distance(rb.position, target.position) < targetDist)
        {
            rb.velocity = Vector3.zero;
            anim.SetBool("Attacking", true);
            attacking = true;
        }
        else
        {
            foreach (GameObject hurtbox in hurtboxes) { hurtbox.SetActive(false); }
            anim.SetBool("Attacking", false);
            attacking = false;
        }
    }
}
