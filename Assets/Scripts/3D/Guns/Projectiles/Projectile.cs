using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public void SetData(float damage, Quaternion rotation, Vector3 direction, float speed, Vector3 pos)
    {
        this.damage = damage;
        this.rotation = rotation;
        this.direction = direction;
        this.speed = speed;
        this.pos = pos;
        start = true;
    }

    internal float damage;
    internal Quaternion rotation;
    internal Vector3 direction;
    internal float speed;
    internal Vector3 pos;
    internal Vector3 lastPos;
    internal bool start;
    internal bool itHit = false;

    void LateUpdate()
    {
        Shoot();
        Aim();
    }

    internal virtual void Shoot()
    {
        if (start)
        {
            speed = speed * 100;
            transform.position = pos;
            GetComponent<Rigidbody>().velocity = direction * speed;

            transform.rotation = rotation;
            start = false;
        }
        RaycastHit[] hits = Physics.RaycastAll(new Ray(lastPos, (transform.position - lastPos).normalized), (transform.position - lastPos).magnitude);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.isTrigger == false && hit.collider.gameObject.tag != "OuterWall" && hit.collider.gameObject.tag != "Player")
            {
                if (hit.collider.gameObject.GetComponent<Health>() != null)
                {
                    hit.collider.gameObject.GetComponent<Health>().TakeDamage(damage);
                }
                itHit = true;
            }
        }
        if (itHit) Kill();
        lastPos = transform.position;
    }
    internal virtual void Aim()
    {
        transform.rotation.SetLookRotation(GetComponent<Rigidbody>().velocity);
    }
    internal virtual void Kill()
    {
        Destroy(gameObject);
    }
}
