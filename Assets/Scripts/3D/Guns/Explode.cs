using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{

    public ParticleSystem part1, part2;
    public float power = 8f, radius = 4f, upForce = 1f;
    public void Wee(float damage, Vector3 pos)
    {
        transform.position = pos;
        
        part1.Play();
        part2.Play();
        
        Collider[] nearby = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider thisGuy in nearby)
        {
            Rigidbody rb = thisGuy.GetComponent<Rigidbody>();
            if (rb != null&&thisGuy.GetComponent<AutoGun>() == null) { if (rb.gameObject.tag != "2D") rb.AddExplosionForce(power, transform.position, radius, upForce, ForceMode.Impulse); }
            
            Health health = thisGuy.GetComponent<Health>();
            if (health != null && thisGuy.GetComponent<AutoGun>() == null) { health.TakeDamage(damage); }
        }
        
    }
}
