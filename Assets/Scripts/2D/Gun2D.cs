using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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
    Animator anim;
    public int clipSize;
    int ammo;
    bool steaming;

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
            anim.SetBool("Shoot", false);
            ammoSlider.value = 0;
            Reload(reloadTime);
        }
        else
        {
            ammoSlider.maxValue = clipSize;
            ammoSlider.value = ammo;
        }

        if (done&&!steaming)
        {

            anim.SetBool("Shoot", false);
            if (InputSystem.GetDevice<Keyboard>().cKey.isPressed)
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
            ammo = clipSize;
        }
        else
        {
            done = true;
        }
    }
    void FirePistol()
    {
        Camera.main.gameObject.GetComponentInParent<AudioManager>().sfx[0].Play();
        Instantiate(bullet, firePoint.position, firePoint.rotation).GetComponent<Bullet2D>().SetData((int)(damage * player.GetComponent<Stats>().baseDamage * damageMultiplier), player.GetComponent<Stats>().critChance, firePoint.rotation, firePoint.forward, speed, firePoint.position, axis);
        anim.SetBool("Shoot", true);


    }
    void FireShotgun()
    {
        Camera.main.gameObject.GetComponentInParent<AudioManager>().sfx[1].Play();
        for (int i = 0; i <= 6; i++) Instantiate(bullet, firePoint.position, firePoint.rotation).GetComponent<Bullet2D>().SetData((int)(damage * player.GetComponent<Stats>().baseDamage * damageMultiplier), player.GetComponent<Stats>().critChance, firePoint.rotation, InaccuracyCalc(), speed, firePoint.position, axis);

        anim.SetBool("Shoot", true);

    }
    void FireBoomer()
    {
        Camera.main.gameObject.GetComponentInParent<AudioManager>().sfx[2].Play();
        Instantiate(bullet, firePoint.position, firePoint.rotation).GetComponent<Boomerang2D>().SetData((int)(damage * player.GetComponent<Stats>().baseDamage * damageMultiplier), player.GetComponent<Stats>().critChance, firePoint.rotation, firePoint.forward, speed, firePoint.position, axis, player);
        anim.SetBool("Shoot", true);


    }
    void FireExploder()
    {
        Camera.main.gameObject.GetComponentInParent<AudioManager>().sfx[2].Play();
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
