using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealDamage : MonoBehaviour
{
    public float upForce = 80;
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
    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() != null && damaged) { damaged = false; }
    }
    void DealKnockback(Collider other)
    {
        if (other.gameObject.GetComponent<ThirdPersonMovement>() != null)
        {
            other.gameObject.GetComponent<ThirdPersonMovement>().clampDisabled = true;
            other.gameObject.GetComponent<ThirdPersonMovement>().knockCount = 0.25f;
        }
        else if (other.gameObject.GetComponent<CharacterController2D>() != null)
        {
            other.gameObject.GetComponent<CharacterController2D>().clampDisabled = true;
            other.gameObject.GetComponent<CharacterController2D>().knockCount = 0.25f;
        }
        else
        {
            return;
        }
        //Debug.Log(((other.transform.position - transform.position).normalized  - new Vector3(0, upForce, 0)).normalized * knockback);
        other.attachedRigidbody.velocity += ((other.transform.position - transform.position).normalized - new Vector3(0, upForce, 0)).normalized * knockback;
        
    }
}

