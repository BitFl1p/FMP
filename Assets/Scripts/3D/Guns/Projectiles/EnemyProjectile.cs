using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    internal override void Shoot()
    {
        if (!start)
        {
            RaycastHit[] hits = Physics.RaycastAll(new Ray(lastPos, (transform.position - lastPos).normalized), (transform.position - lastPos).magnitude);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.isTrigger == false && hit.collider.gameObject.tag != "OuterWall" && hit.collider.gameObject.tag != "Enemy")
                {
                    if (hit.collider.gameObject.GetComponent<PlayerHealth>() != null)
                    {
                        float rand = Random.Range(0, 100);
                        if (rand <= critChance) hit.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage * 2);
                        else hit.collider.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);

                    }
                    itHit = true;
                }
            }
        }
        if (start)
        {
            speed = speed * 100;
            transform.position = pos;
            GetComponent<Rigidbody>().velocity = direction * speed;

            transform.rotation = rotation;
            start = false;
        }

        if (itHit) Kill();
        lastPos = transform.position;
    }
}
