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
    public Explode explosion;
    bool detected;


    private void Update()
    {
        if (done) anim.SetBool("Shoot", false); //anim.enabled = false;
    }
    void Shoot()
    {

        muzzle?.Play();
        Instantiate(bullet).GetComponent<Bullet>().SetData(damage, firePoint.rotation, firePoint.forward, speed, firePoint.position);
        anim.SetBool("Shoot", true);

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<Health>() != null)
        {

            head.LookAt(FindObjectOfType<Health>().transform.position);
            if (done)
            {
                anim.SetBool("Shoot", false);
                done = false;
                Shoot();
            }

        }
    }

    public void Explode()
    {
        Instantiate(explosion).Wee(damage, transform.position); ;
        Destroy(gameObject);
    }
}