using UnityEngine;
using UnityEngine.UI;

public class Gun2D : GunBase
{
    public float critChance;
    public GameObject player;
    string axis;
    public Slider ammoSlider;
    float reloadCount;
    public float reloadTime;
    public ParticleSystem[] steam;
    public GameObject bullet;
    public Explode2D explode;
    public float speed;
    public Transform firePoint;
    public bool done = true;
    Animator anim;
    public int clipSize;
    int ammo;
    bool reload, steaming;

    void Start()
    {
        ammo = clipSize;
        anim = GetComponent<Animator>();
        axis = player.GetComponent<CharacterController2D>().axis;
    }
    void Update()
    {
        if (clipSize <= 0)
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
                    case 6: ThrowSentry(); break;
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

        Instantiate(bullet, firePoint.position, firePoint.rotation).GetComponent<Bullet2D>().SetData((int)(damage * player.GetComponent<Stats>().baseDamage * damageMultiplier), player.GetComponent<Stats>().critChance, firePoint.rotation, firePoint.forward, speed, firePoint.position, axis);
        anim.SetBool("Shoot", true);


    }
    void FireShotgun()
    {

        for (int i = 0; i <= 6; i++) Instantiate(bullet, firePoint.position, firePoint.rotation).GetComponent<Bullet2D>().SetData((int)(damage * player.GetComponent<Stats>().baseDamage * damageMultiplier), player.GetComponent<Stats>().critChance, firePoint.rotation, InaccuracyCalc(), speed, firePoint.position, axis);

        anim.SetBool("Shoot", true);

    }
    void FireBoomer()
    {

        Instantiate(bullet, firePoint.position, firePoint.rotation).GetComponent<Boomerang2D>().SetData((int)(damage * player.GetComponent<Stats>().baseDamage * damageMultiplier), player.GetComponent<Stats>().critChance, firePoint.rotation, firePoint.forward, speed, firePoint.position, axis, player);
        anim.SetBool("Shoot", true);


    }
    void FireExploder()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation).GetComponent<ExplodeBullet2D>().SetData((int)(damage * player.GetComponent<Stats>().baseDamage * damageMultiplier), player.GetComponent<Stats>().critChance, firePoint.rotation, firePoint.forward, speed, firePoint.position, axis, explode);
        anim.SetBool("Shoot", true);

    }
    void ThrowSentry()
    {
        Instantiate(bullet, firePoint.position, firePoint.rotation).GetComponent<SentryCase2D>().SetData((int)(damage * player.GetComponent<Stats>().baseDamage * damageMultiplier), player.GetComponent<Stats>().critChance, firePoint.rotation, firePoint.forward, speed, firePoint.position, axis);
        anim.SetBool("Shoot", true);
    }
    Vector3 InaccuracyCalc() { return new Vector3(firePoint.forward.x, UnityEngine.Random.Range(-0.1f, 0.1f), firePoint.forward.z).normalized; }
}
