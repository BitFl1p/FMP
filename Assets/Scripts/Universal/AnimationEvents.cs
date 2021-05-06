using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public Gun2D gun2D;
    public ZapCannon zap;
    public ZapCannon2D zap2D;
    public Gun[] gun;
    public AutoGun sentry;
    public AutoGun2D sentry2D;
    public GameObject entling;
    public Transform firePoint;
    public float speed;
    public EnemyProjectile bullet;
    public GameObject self;
    public EnemyArrow arrow;
    public EnemyExplodeBullet explodeBullet;
    public EnemyExplode explode;
    public Transform firePoint2;
    public void DieMore()
    {
        Destroy(self);
    }
    public void Explode()
    {
        Collider[] nearby = Physics.OverlapSphere(transform.position, 100);
        foreach (Collider thisGuy in nearby)
        {
            Rigidbody rb = thisGuy.GetComponent<Rigidbody>();
            if (rb != null && thisGuy.GetComponent<AutoGun>() == null && rb.gameObject != self) { if (rb.gameObject.tag != "2D") rb.AddExplosionForce(self.GetComponent<TeslaSpider>().knockback, transform.position, 80, 18f, ForceMode.Impulse); }
            if (thisGuy.GetComponent<PlayerHealth>()) thisGuy.GetComponent<PlayerHealth>().TakeDamage(self.GetComponent<TeslaSpider>().damage);
        }
    }
    public void GunDone()
    {
        foreach (Gun goon in gun)
        {
            goon.done = true;
        }

    }
    public void ZapDone()
    {

        if (zap != null) zap.done = true;
        if (zap2D != null) zap2D.done = true;
    }
    public void SentryDone()
    {
        if (sentry != null) sentry.done = true;
        if (sentry2D != null) sentry2D.done = true;
    }
    public void SpawnSentry()
    {

        if (sentry != null)
        {
            AutoGun instance = Instantiate(sentry);
            instance.transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
            instance.damage = GetComponent<SentryCase>().damage;
        }
        if (sentry2D != null)
        {
            AutoGun2D instance = Instantiate(sentry2D);
            instance.transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z);
            instance.axis = GetComponent<SentryCase2D>().axis;
            instance.damage = GetComponent<SentryCase2D>().damage;
        }


    }
    public void Die()
    {
        Destroy(gameObject);
    }
    public void Gun2DDone()
    {
        gun2D.done = true;
    }
    public void SpitEntling()
    {
        GameObject ling = Instantiate(entling);
        ling.GetComponentInChildren<Rigidbody>().gameObject.transform.position = firePoint.position;
        ling.GetComponentInChildren<Rigidbody>().velocity = firePoint.forward * speed;
    }
    public void SpitBullet()
    {
        Projectile instance = Instantiate(bullet);
        firePoint.LookAt(GetComponentInParent<EnemyAI>().target);
        instance.SetData(GetComponentInParent<EnemyAI>().damage, 0, firePoint.rotation, firePoint.forward, speed, firePoint.position);
    }
    public void ShootArrow()
    {
        EnemyArrow instance = Instantiate(arrow);
        instance.SetData(GetComponentInParent<EnemyAI>().damage, 0, firePoint.rotation, firePoint.forward, speed, firePoint.position, GetComponentInParent<EnemyAI>().target);
    }
    public void ShootExploder()
    {
        EnemyExplodeBullet instance = Instantiate(explodeBullet, firePoint.position, firePoint.rotation);
        instance.SetData(GetComponentInParent<EnemyAI>().damage, 0, firePoint.rotation, firePoint.forward, speed, firePoint.position, explode);
    }
    public void SummonTwo()
    {
        GameObject ling = Instantiate(entling);
        ling.GetComponentInChildren<Rigidbody>().gameObject.transform.position = firePoint2.position;
        ling.GetComponentInChildren<Rigidbody>().velocity = firePoint2.forward * speed;
    }
}

