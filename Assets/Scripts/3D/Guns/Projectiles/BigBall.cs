using UnityEngine;

public class BigBall : Projectile
{
    int count = 0;
    float damCount = 1;
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
        if (health)
        {
            health.TakeDamage(damage);
            count++;
            damCount = 1;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        damCount -= Time.deltaTime;
        if(damCount < 0) OnTriggerEnter(other);

    }


}
