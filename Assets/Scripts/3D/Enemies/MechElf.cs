using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechElf : EnemyAI
{
    internal override void FixedUpdate()
    {
        anim.SetBool("Schmove", true); transform.LookAt(transform.position + rb.velocity); transform.eulerAngles += new Vector3(0, 90, 0);
        Attack();
        PlayerSeen();
        //transform.LookAt(rb.velocity.normalized);

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
    }
    internal override void Attack()
    {

        if (Vector3.Distance(rb.position, target.position) < targetDist)
        {
            anim.SetBool("Attacking", true);
        }
        else
        {
            anim.SetBool("Attacking", false);
        }
    }
}
