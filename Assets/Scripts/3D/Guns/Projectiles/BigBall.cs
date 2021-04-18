using UnityEngine;

public class BigBall : Projectile
{
    int count = 0;
    internal override void Shoot()
    {
        if (start)
        {
            speed = speed * 100;
            transform.position = pos;
            transform.rotation = rotation;
            start = false;
        }
        transform.position += direction * speed;
        lastPos = transform.position;

        if (count >= 3) { GetComponent<Die>().Deth(); }
    }
    internal override void Aim()
    {
        transform.rotation.SetLookRotation(direction);
    }
    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        health?.TakeDamage(damage);
        count++;
    }


}
