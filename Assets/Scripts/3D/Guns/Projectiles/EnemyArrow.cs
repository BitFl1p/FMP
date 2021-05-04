using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : EnemyProjectile
{
    Transform target;
    public void SetData(int damage, float critChance, Quaternion rotation, Vector3 direction, float speed, Vector3 pos, Transform target)
    {
        SetData(damage, critChance, rotation, direction, speed, pos);
        this.target = target;
    } 
    internal override void Shoot()
    {
        if (!start)
        {
            Vector3 targetDelta = target.position - transform.position;

            //get the angle between transform.forward and target delta
            float angleDiff = Vector3.Angle(transform.forward, targetDelta);

            // get its cross product, which is the axis of rotation to
            // get from one vector to the other
            Vector3 cross = Vector3.Cross(transform.forward, targetDelta);

            // apply torque along that axis according to the magnitude of the angle.
            GetComponent<Rigidbody>().AddTorque(cross * angleDiff * speed);
            //direction = Vector3.Cross(direction, target.position - transform.position).normalized;
            //GetComponent<Rigidbody>().velocity = direction * speed;
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
