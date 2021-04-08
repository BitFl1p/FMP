using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang2D : Projectile2D
{
    public void SetData(int damage, float critChance, Quaternion rotation, Vector3 direction, float speed, Vector3 pos,string axis, GameObject player)
    {
        SetData(damage, critChance, rotation, direction, speed, pos, axis);
        this.player = player;
    }
    internal GameObject player;
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
        if(axis == "XY") GetComponent<Rigidbody>().velocity += new Vector3(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y, 0);
        else GetComponent<Rigidbody>().velocity += new Vector3(0, player.transform.position.y - transform.position.y, player.transform.position.z - transform.position.z);
        GetComponent<Rigidbody>().velocity += new Vector3(0, speed / 1000, 0);
        RaycastHit[] hits = Physics.RaycastAll(new Ray(lastPos, (transform.position - lastPos).normalized), (transform.position - lastPos).magnitude);
        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.isTrigger == false && hit.collider.gameObject.tag != "OuterWall" && hit.collider.gameObject.tag != "Player")
            {
                if (hit.collider.gameObject.GetComponent<Health>() != null)
                {
                    if (Random.Range(0, 100) <= critChance) hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage * 2);
                    else hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(damage);
                }
                itHit = true;
            }
        }
        if (itHit) Kill();
        lastPos = transform.position;
    }
    internal override void Aim()
    {

        if (axis == "XY") transform.eulerAngles = new Vector3(0, 0, 0);
        else transform.eulerAngles = new Vector3(0, 90, 0);
    }
}
