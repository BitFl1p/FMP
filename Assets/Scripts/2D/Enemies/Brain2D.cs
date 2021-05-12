using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain2D : EnemyAI2D
{
    internal override void PlayerSeen()
    {

        Vector3 targetDir = (target.position - transform.position).normalized;
        if (target.gameObject.activeInHierarchy)
        {

            if (axis == "ZY")
            {
                rb.velocity = new Vector3(0, Mathf.Clamp(targetDir.y * speed, speed * -10, speed * 10), Mathf.Clamp(targetDir.z * speed, speed * -10, speed * 10));
                if (targetDir.y >= 135 && targetDir.y <= 225) { transform.eulerAngles = new Vector3(0, 90, 0); }
                else { transform.eulerAngles = new Vector3(0, -90, 0); }
            }
            else
            {
                rb.velocity = new Vector3(Mathf.Clamp(targetDir.x * speed, speed * -10, speed * 10), Mathf.Clamp(targetDir.y * speed, speed * -10, speed * 10), 0);
                if (targetDir.y >= 45 && targetDir.y <= 135) { transform.eulerAngles = new Vector3(0, 180, 0); }
                else { transform.eulerAngles = new Vector3(0, 0, 0); }
            }
        }
    }
    internal override void Attack()
    {
        foreach (GameObject hurtbox in hurtboxes) { hurtbox.GetComponent<DealDamage>().damage = damage; hurtbox.GetComponent<DealDamage>().knockback = knockback; }
    }
}
