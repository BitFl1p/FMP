using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplode : Explode
{
    public new void Wee(int damage, Vector3 pos)
    {
        Camera.main.gameObject.GetComponentInParent<AudioManager>().sfx[4].Play();
        transform.position = pos;

        part1.Play();
        part2.Play();

        Collider[] nearby = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider thisGuy in nearby)
        {
            Rigidbody rb = thisGuy.GetComponent<Rigidbody>();
            if (rb != null && thisGuy.GetComponent<AutoGun>() == null) { if (rb.gameObject.tag != "2D") rb.AddExplosionForce(power, transform.position, radius, upForce, ForceMode.Impulse); }

            Health health = thisGuy.GetComponent<Health>();
            if (health != null && thisGuy.GetComponent<AutoGun>() == null) if (thisGuy.gameObject.tag == "Enemy") thisGuy.GetComponent<PlayerHealth>().TakeDamage((int)Mathf.Ceil(damage * 0.6f)); else health.TakeDamage(damage);
        }

    }
}
