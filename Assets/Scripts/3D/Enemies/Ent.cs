using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Ent : Entling
{
    float count = 6;
    public float[] maxCount;
    internal override void Attack()
    {
        if (reachedEndOfPath)
        {
            foreach (GameObject hurtbox in hurtboxes) { hurtbox.GetComponent<DealDamage>().damage = damage; }
            anim.SetBool("Attacking", true);
        }
        else
        {
            if(count <= 0)
            {
                anim.SetBool("Yeet", true);
                count = Random.Range(maxCount[0], maxCount[1]);
            }
            else
            {
                anim.SetBool("Yeet", false);
                count -= Time.deltaTime;
            }
            foreach (GameObject hurtbox in hurtboxes) { hurtbox.SetActive(false); }
            anim.SetBool("Attacking", false);
        }
    }

}
