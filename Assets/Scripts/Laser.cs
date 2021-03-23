using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public int damage;
    float count;
    public float timer;
    bool dealDamage;
    private void OnEnable()
    {
        count = timer;
    }
    private void Update()
    {
        count -= Time.deltaTime;
        if (count <= 0)
        {
            dealDamage = true;
            count = timer;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Health>() != null && dealDamage) { other.gameObject.GetComponent<Health>().TakeDamage(damage); dealDamage = false; }
    }
}
