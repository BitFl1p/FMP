using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnDamage : MonoBehaviour
{
    public int damage;
    public bool damaged = false;
    public float knockback;
    float count;
    private void OnEnable()
    {
        damaged = false;
    }
    private void OnCollisionEnter(Collision other) { Damage(other); }
    private void OnCollisionStay(Collision other) { Damage(other); }

    private void Update()
    {
        if (damaged)
        {
            count -= Time.deltaTime;
            if (count <= 0)
            {
                damaged = false;
            }
        }
    }
    void Damage(Collision other)
    {
        if (other.gameObject.GetComponent<PlayerHealth>() != null && !damaged)
        {
            //other.gameObject.GetComponent<Rigidbody>().velocity += (transform.position - other.transform.position).normalized * knockback;
            other.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);
            damaged = true;
            count = 0.5f;
            other.gameObject.GetComponent<Rigidbody>().AddExplosionForce(knockback, other.transform.position - new Vector3(0,10,0), 100, 80, ForceMode.Impulse);
        }
    }
}

