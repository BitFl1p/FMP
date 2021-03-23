using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    internal void SetData(int damage, Quaternion rotation, Vector3 direction, float speed, Vector3 pos)
    {
        this.damage = damage;
        this.rotation = rotation;
        this.direction = direction;
        this.speed = speed;
        this.pos = pos;
        start = true;
    }

    internal int damage;
    internal Quaternion rotation;
    internal Vector3 direction;
    internal float speed;
    internal Vector3 pos;
    internal Vector3 lastPos;
    internal bool start;
    internal bool destroy = false;

    private void LateUpdate()
    {
        Shoot();
        Aim();
    }
    internal void Shoot()
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
            
            if (hit.collider.isTrigger == false)
            {
                destroy = true;
                if (hit.collider.gameObject.GetComponent<Health>() != null)
                {
                    hit.collider.gameObject.GetComponent<Health>().TakeDamage(damage);
                }
            }
        }
        
        lastPos = transform.position;
        

    }

    internal virtual void Aim()
    {
        transform.rotation.SetLookRotation(GetComponent<Rigidbody>().velocity);
    }
}
