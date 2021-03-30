using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public List<GameObject> hits;
    public List<GameObject> obsolhits;
    public float damage;
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
        if (other.gameObject.GetComponent<Health>() != null && dealDamage) { foreach(GameObject hit in hits) hit.gameObject.GetComponent<Health>().TakeDamage(damage); dealDamage = false; }
        foreach (GameObject hit in hits) if (!hit.activeInHierarchy) obsolhits.Add(hit);
        foreach (GameObject hit in obsolhits) hits.Remove(hit);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy") hits.Add(other.gameObject);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy") hits.Remove(other.gameObject);
    }
}
