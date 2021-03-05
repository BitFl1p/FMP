using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{

    public ParticleSystem part1, part2;
    public float power = 8f, radius = 4f, upForce = 1f;
    public void Wee(int damage, Vector3 pos)
    {
        transform.position = pos;
        Debug.Log("exist");
        part1.Play();
        part2.Play();
        Debug.Log("exist");
        Collider[] nearby = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider thisGuy in nearby)
        {
            Rigidbody rb = thisGuy.GetComponent<Rigidbody>();
            if (rb != null) { rb.AddExplosionForce(power, transform.position, radius, upForce, ForceMode.Impulse); }
            
            Health health = thisGuy.GetComponent<Health>();
            if (health != null) { health.TakeDamage(damage); }
        }
        
    }
}
