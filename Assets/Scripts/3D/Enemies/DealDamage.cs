using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public int damage;
    public bool damaged = false;
    public float knockback;
    private void OnEnable()
    {
        damaged = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() != null && !damaged)
        {
            other.attachedRigidbody.velocity += (transform.position - other.transform.position).normalized * knockback;
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            damaged = true;
            other.GetComponent<Rigidbody>().AddExplosionForce(knockback, transform.position, 100, 80, ForceMode.Impulse);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() != null && !damaged)
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            damaged = true;
            
            other.GetComponent<Rigidbody>().AddExplosionForce(knockback, transform.position, 100, 80, ForceMode.Impulse); 
        }
    }
}

