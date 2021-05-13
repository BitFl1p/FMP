using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechElf2D : EnemyAI2D
{
    internal override void Attack() { }
    internal override void PlayerSeen()
    {
        if (target == null) return;
        Vector3 targetDir = Quaternion.LookRotation((target.position - transform.position).normalized, Vector3.up).eulerAngles;
        if (target.gameObject.activeInHierarchy)
        {
            if (Vector3.Distance(rb.position, target.position) < targetDist)
            {
                foreach (GameObject hurtbox in hurtboxes) { hurtbox.GetComponent<DealDamage>().damage = damage; hurtbox.GetComponent<DealDamage>().knockback = knockback; }
                anim.SetBool("Attacking", true);
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
                return;
            }
            else
            {
                foreach (GameObject hurtbox in hurtboxes) { hurtbox.SetActive(false); }
                anim.SetBool("Attacking", false);
            }
            if (axis == "ZY")
            {
                if (targetDir.y >= 135 && targetDir.y <= 225)
                {
                    rb.velocity -= new Vector3(0, 0, 1) * speed;
                    transform.eulerAngles = new Vector3(0, 90, 0);
                }
                else
                {
                    rb.velocity += new Vector3(0, 0, 1) * speed;
                    transform.eulerAngles = new Vector3(0, -90, 0);
                }

                rb.velocity = new Vector3(0, rb.velocity.y, Mathf.Clamp(rb.velocity.z, speed * -10, speed * 10));
            }
            else
            {
                if (targetDir.y >= 45 && targetDir.y <= 135)
                {
                    rb.velocity -= new Vector3(1, 0, 0) * speed;
                    transform.eulerAngles = new Vector3(0, 180, 0);
                }
                else
                {
                    rb.velocity += new Vector3(1, 0, 0) * speed;
                    transform.eulerAngles = new Vector3(0, 0, 0);
                }

                rb.velocity = new Vector3(Mathf.Clamp(rb.velocity.x, speed * -10, speed * 10), rb.velocity.y, 0);

            }
            if (walled && isGrounded)
            {
                rb.velocity += new Vector3(0, speed * 5, 0);
            }

        }
    }
}
