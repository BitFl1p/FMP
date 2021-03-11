using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGun : MonoBehaviour
{
    public Transform head;
    public GameObject bullet;
    public ParticleSystem muzzle;
    public float speed;
    public Transform firePoint;
    public bool done = true;
    public int damage = 1;
    public Animator anim;


    private void Update()
    {
        if(done) anim.SetBool("Shoot", false);
    }
    void Shoot()
    {

        muzzle?.Play();
        Instantiate(bullet).GetComponent<Bullet>().SetData(damage, firePoint.rotation, firePoint.forward, speed, firePoint.position);
        anim.SetBool("Shoot", true);


    }
    private void OnTriggerStay(Collider other)
    {
        if (done&&other.gameObject.GetComponent<Health>() != null)
        {
            anim.SetBool("Shoot", false);
            head.LookAt(FindObjectOfType<Health>().transform.position);
            done = false;
            Shoot();
        }       
    }
}
