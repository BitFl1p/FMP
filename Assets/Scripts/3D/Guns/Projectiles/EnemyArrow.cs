using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : EnemyProjectile
{
    internal override void Shoot()
    {
        if (!start)
        {
            
            GetComponent<Rigidbody>().velocity = direction * speed;
            RaycastHit[] hits = Physics.RaycastAll(new Ray(lastPos, (transform.position - lastPos).normalized), (transform.position - lastPos).magnitude);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.isTrigger == false && hit.collider.gameObject.tag != "OuterWall")
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
