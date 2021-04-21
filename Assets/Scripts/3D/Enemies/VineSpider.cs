using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineSpider : EnemyAI
{
    float count = 4;
    public float[] maxCount;
    internal override void FixedUpdate()
    {
        base.FixedUpdate();
        transform.LookAt(target);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        transform.eulerAngles += new Vector3(0, 90, 0);
       
    }
    internal override void Attack()
    {
        if (reachedEndOfPath)
        {
            foreach (GameObject hurtbox in hurtboxes) { hurtbox.GetComponent<DealDamage>().damage = damage; }
            anim.SetBool("Attacking", true);
        }
        else
        {
            if (count <= 0)
            {
                anim.SetBool("SpittinStraightFacts", true);
                count = Random.Range(maxCount[0],maxCount[1]);
            }
            else
            {
                anim.SetBool("SpittinStraightFacts", false);
                count -= Time.deltaTime;
            }
            foreach (GameObject hurtbox in hurtboxes) { hurtbox.SetActive(false); }
            anim.SetBool("Attacking", false);
        }
    }
}
