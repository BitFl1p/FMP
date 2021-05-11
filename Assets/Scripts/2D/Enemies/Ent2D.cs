using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ent2D : EnemyAI2D
{
    float count;
    internal override void Attack()
    {
        if (Vector3.Distance(rb.position, target.position) < targetDist && target.gameObject.activeInHierarchy)
        {
            foreach (GameObject hurtbox in hurtboxes) { hurtbox.GetComponent<DealDamage>().damage = damage; hurtbox.GetComponent<DealDamage>().knockback = knockback; }
            if (count <= 0)
            {
                switch (Random.Range(1, 4))
                {
                    case 1:
                        count = 6;
                        anim.SetBool("Attacking2", false);
                        anim.SetBool("Attacking1", true);
                        break;
                    case 2:
                        count = 6;
                        anim.SetBool("Attacking1", false);
                        anim.SetBool("Attacking2", true);
                        break;
                    case 3:
                        count = 6;
                        anim.SetBool("Attacking1", true);
                        anim.SetBool("Attacking2", true);
                        break;
                }
            }
            else { count -= Time.deltaTime; }
            if (count <= 2) { anim.SetBool("Attacking1", false); anim.SetBool("Attacking2", false); }
        }
        else
        {
            foreach (GameObject hurtbox in hurtboxes) { hurtbox.SetActive(false); }
            anim.SetBool("Attacking1", false);
            anim.SetBool("Attacking2", false);
        }
    }
}
