using UnityEngine;

public class Boomerang : Projectile
{
    public void SetData(int damage, float critChance, Quaternion rotation, Vector3 direction, float speed, Vector3 pos, GameObject player)
    {
        SetData(damage, critChance, rotation, direction, speed, pos);
        this.player = player;
    }
    internal GameObject player;
    internal override void Shoot()
    {
        if (start)
        {
            speed = speed * 100;
            transform.position = pos;
            GetComponent<Rigidbody>().velocity = direction * speed;

            transform.rotation = rotation;
            start = false;
        }
        GetComponent<Rigidbody>().velocity += player.transform.position - transform.position;
        GetComponent<Rigidbody>().velocity += new Vector3(0, speed / 1000, 0);
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
    internal override void Aim()
    {

        transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, transform.eulerAngles.z);
        transform.eulerAngles += new Vector3(0, -30, 0);
    }

}
