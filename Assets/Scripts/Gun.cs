using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public Slider ammoSlider;
    float reloadCount;
    public float reloadTime;
    public ParticleSystem[] steam;
    public GameObject bullet,explode;
    public ParticleSystem muzzle;
    public float speed;
    public Transform firePoint;
    public bool done = true;
    public int damage = 1;
    float timer;
    public Animator anim;
    public int wepNum, clipSize;
    int ammo;
    bool reload, steaming;
    void Start()
    {
        ammo = clipSize;
    }
    void Update()
    {

        if (ammo <= 0 && !(reloadTime <= 0))
        {
            ammoSlider.value = ammoSlider.maxValue;
            reload = true;
            Reload(reloadTime);
        }
        else
        {
            ammoSlider.maxValue = clipSize;
            ammoSlider.value = ammo;
        }
        anim.SetInteger("WepNum", wepNum);
        if(done)
        {
            
            anim.SetBool("Shoot", false);
            if (Input.GetButton("Fire1") && !reload)
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

        muzzle?.Play();
        Instantiate(bullet).GetComponent<Bullet>().SetData(damage, firePoint.rotation, firePoint.forward, speed, firePoint.position);
        anim.SetBool("Shoot", true);
        

    }
    void FireShotgun()
    {

        muzzle?.Play();
        for (int i = 0; i <= 6; i++) 
            Instantiate(bullet).GetComponent<Bullet>().SetData
                (
                    damage, 
                    firePoint.rotation, 
                    InaccuracyCalc(), 
                    speed, 
                    firePoint.position
                );

        anim.SetBool("Shoot", true);

    }
    void FireBoomer()
    {

        muzzle?.Play();
        Instantiate(bullet).GetComponent<Boomerang>().SetData(damage, firePoint.rotation, firePoint.forward, speed, firePoint.position);
        anim.SetBool("Shoot", true);
        

    }
    void FireExploder()
    {
        muzzle?.Play();
        Instantiate(bullet).GetComponent<ExplodeBullet>().SetData(damage, firePoint.rotation, firePoint.forward, speed, firePoint.position,explode);
        anim.SetBool("Shoot", true);
        
    }
    void ThrowSentry()
    {
        Instantiate(bullet).GetComponent<SentryCase>().SetData(damage, firePoint.rotation, firePoint.forward, speed, firePoint.position);
        anim.SetBool("Shoot", true);
    }
    Vector3 InaccuracyCalc() { return new Vector3(firePoint.forward.x + (firePoint.forward.x * Random.Range(-0.1f, 0.1f)), firePoint.forward.y + (firePoint.forward.y * Random.Range(-0.3f, 0.3f)), firePoint.forward.z + (firePoint.forward.z * Random.Range(-0.1f, 0.1f))).normalized; }
}
