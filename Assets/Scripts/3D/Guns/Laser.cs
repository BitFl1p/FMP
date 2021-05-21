using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float damageMultiplier;
    public List<GameObject> hits;
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
        
        if (other.gameObject.GetComponent<Health>()) 
        {
            if (dealDamage)
            {
                hits.RemoveAll(item => item == null);
                foreach (GameObject hit in hits) hit.gameObject.GetComponent<Health>().TakeDamage(damage);
                dealDamage = false;
            }
            hits.RemoveAll(item => item == null);
            if ((other.gameObject.tag == "Enemy" || other.gameObject.tag == "Enemy2D") && !hits.Contains(other.gameObject)) hits.Add(other.gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        hits.RemoveAll(item => item == null);
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Enemy2D") hits.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        hits.RemoveAll(item => item == null);
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Enemy2D") hits.Remove(other.gameObject);
    }
}
