using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile2D : Projectile
{
    public void SetData(int damage, Quaternion rotation, Vector3 direction, float speed, Vector3 pos, string axis)
    {
        SetData(damage, rotation, direction, speed, pos);
        this.axis = axis;
    }


    internal string axis;
    private void OnEnable()
    {
        if (axis == "XY") GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
        else GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
    }

    internal override void Shoot()
    {
        if (start)
        {
            speed = speed * 100;
            transform.position = pos;
            if (axis == "XY") GetComponent<Rigidbody>().velocity = new Vector3(direction.x * speed, direction.y * speed, 0);
            else GetComponent<Rigidbody>().velocity = new Vector3(0, direction.y * speed, direction.z * speed);
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
    
}