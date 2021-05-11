using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public int upForce = 80;
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
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            damaged = true;
            //Debug.Log((new Vector3(transform.position.z * 3, (-transform.position.x - transform.position.z) / 20, transform.position.z * 3) - other.transform.position).normalized * 10);
            DealKnockback(other);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() != null && !damaged)
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            damaged = true;
            //Debug.Log((new Vector3(transform.position.x, (-transform.position.x - transform.position.z) / 20, transform.position.z) - other.transform.position).normalized * 3);
            DealKnockback(other); 
        }
    }
    void DealKnockback(Collider other)
    {
        other.gameObject.GetComponent<CharacterController2D>().clampDisabled = true;
        other.gameObject.GetComponent<CharacterController2D>().knockCount = 1;
        Debug.Log((other.transform.position - transform.position).normalized - (other.transform.position - transform.position).normalized / 2 + new Vector3(0, 0.5f, 0) * knockback);
        other.attachedRigidbody.velocity += (other.transform.position - transform.position).normalized - (other.transform.position - transform.position).normalized/ 2 + new Vector3(0,0.5f,0) * knockback;
        
    }
}

