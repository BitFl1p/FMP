using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile2D : MonoBehaviour
{
    internal void SetData(int damage, Quaternion rotation, Vector3 direction, float speed, Vector3 pos, string axis)
    {
        this.damage = damage;
        this.rotation = rotation;
        this.direction = direction;
        this.speed = speed;
        this.pos = pos;
        this.axis = axis;
        start = true;
    }

    internal int damage;
    internal Quaternion rotation;
    internal Vector3 direction;
    internal float speed;
    internal Vector3 pos;
    internal Vector3 lastPos;
    internal string axis;
    internal bool start;

    internal void Shoot()
    {
        if (start)
        {
            speed = speed * 100;
            transform.position = pos;
            if (axis == "XY") GetComponent<Rigidbody>().velocity = new Vector3(direction.x * speed, direction.y * speed, 0);
            else GetComponent<Rigidbody>().velocity = new Vector3(0, direction.y * speed, direction.x * speed);
            transform.rotation = rotation;
            start = false;
        }
        else
        {
            RaycastHit[] hits = Physics.RaycastAll(new Ray(lastPos, (transform.position - lastPos).normalized), (transform.position - lastPos).magnitude);
            foreach (RaycastHit hit in hits)
            {
                if (hit.collider.isTrigger == false && hit.collider.gameObject.tag != "OuterWall")
                {
                    if (hit.collider.gameObject.GetComponent<Health>() != null)
                    {
                        hit.collider.gameObject.GetComponent<Health>().TakeDamage(damage);
                    }
                    Destroy(gameObject);
                }
            }
            lastPos = transform.position;
            if (axis == "XY") transform.rotation.SetLookRotation(GetComponent<Rigidbody>().velocity);
            else transform.rotation.SetLookRotation(new Vector3(GetComponent<Rigidbody>().velocity.z, GetComponent<Rigidbody>().velocity.y, GetComponent<Rigidbody>().velocity.x));
        }
    }
}
