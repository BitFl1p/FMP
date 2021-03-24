using UnityEngine;
using UnityEngine.UI;
using System;

public class Gun2D : MonoBehaviour
{
    public GameObject player;
    public string axis;
    public Slider ammoSlider;
    float reloadCount;
    public float reloadTime;
    public ParticleSystem[] steam;
    public GameObject bullet;
    public Explode2D explode;
    public float speed;
    public Transform firePoint;
    public bool done = true;
    public int damage = 1;
    float timer;
    Animator anim;
    public int wepNum, clipSize;
    int ammo;
    bool reload, steaming;

    void Start()
    {
        ammo = clipSize;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if(clipSize <= 0)
        {
            ammoSlider.value = ammoSlider.maxValue;
        }
        if (ammo <= 0 && !(reloadTime <= 0))
        {
            ammoSlider.value = 0;
            reload = true;
            Reload(reloadTime);
        }
        else
        {
            ammoSlider.maxValue = clipSize;
            ammoSlider.value = ammo;
        }

        if (done)
        {

            anim.SetBool("Shoot", false);
            if (Input.GetKey(KeyCode.C) && !reload)
            {
                ammo--;
                done = false;
                switch (wepNum)
                {
                    case 0: FirePistol(); break;
                    case 1: FireShotgun(); break;
                    case 2: FireBoomer(); break;
                    case 3: FireExploder(); break;
                    case 6:
                        //ThrowSentry();
                        Debug.LogError("Not Implemented");
                        break;


                }


            }


        }
    }
    void Reload(float reloadTime)
    {
        if (!steaming)
        {
            reloadCount = reloadTime;
            foreach (ParticleSystem current in steam) { current.Play(); }
            steaming = true;
        }
        reloadCount -= Time.deltaTime;
        if (reloadCount <= 0)
        {
            steaming = false;
            reload = false;
            ammo = clipSize;
        }
    }
    void FirePistol()
    {

        Instantiate(bullet).GetComponent<Bullet2D>().SetData(damage, firePoint.rotation, firePoint.forward, speed, firePoint.position, axis);
        anim.SetBool("Shoot", true);


    }
    void FireShotgun()
    {

        for (int i = 0; i <= 6; i++) Instantiate(bullet).GetComponent<Bullet2D>().SetData(damage, firePoint.rotation, InaccuracyCalc(), speed, firePoint.position, axis);

        anim.SetBool("Shoot", true);

    }
    void FireBoomer()
    {

        Instantiate(bullet).GetComponent<Boomerang2D>().SetData(damage, firePoint.rotation, firePoint.forward, speed, firePoint.position, player);
        anim.SetBool("Shoot", true);


    }
    void FireExploder()
    {
        Instantiate(bullet).GetComponent<ExplodeBullet2D>().SetData(damage, firePoint.rotation, firePoint.forward, speed, firePoint.position, axis, explode);
        anim.SetBool("Shoot", true);

    }
    void ThrowSentry()
    {
        Instantiate(bullet).GetComponent<SentryCase>().SetData(damage, firePoint.rotation, firePoint.forward, speed, firePoint.position);
        anim.SetBool("Shoot", true);
    }
    Vector3 InaccuracyCalc() { return new Vector3(firePoint.forward.x, UnityEngine.Random.Range(-0.1f, 0.1f), firePoint.forward.z).normalized; }
}
