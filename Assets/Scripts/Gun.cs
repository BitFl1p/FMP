using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public ParticleSystem muzzle;
    public float speed;
    public Transform firePoint;
    public bool done = true;
    public int damage = 1;
    float timer;
    public Animator anim;
    public int wepNum;
    void Update()
    {
        anim.SetInteger("WepNum", wepNum);
        if(done)
        {
            anim.SetBool("Shoot", false);
            if (Input.GetButton("Fire1"))
            {
                done = false;
                switch (wepNum)
                {
                    case 0:
                        FirePistol();
                        break;
                    case 1:
                        FireShotgun();
                        break;
                    case 2:
                        FireBoomer();
                        break;


                }
            }
            
        }
    }
    void FirePistol()
    {
        
        if (muzzle != null) { muzzle.Play(); }
        Instantiate(bullet).GetComponent<Bullet>().SetData(damage, firePoint.rotation, firePoint.forward, speed, firePoint.position);
        anim.SetBool("Shoot", true);
        //Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.red, 2f);

    }
    void FireShotgun()
    {

        if (muzzle != null) { muzzle.Play(); }
        for(int i = 0; i <= 6; i++) 
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

        if (muzzle != null) { muzzle.Play(); }
        Instantiate(bullet).GetComponent<Boomerang>().SetData(damage, firePoint.rotation, firePoint.forward, speed, firePoint.position);
        anim.SetBool("Shoot", true);
        //Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.red, 2f);

    }
    Vector3 InaccuracyCalc() { return new Vector3(firePoint.forward.x + (firePoint.forward.x * Random.Range(-0.1f, 0.1f)), firePoint.forward.y + (firePoint.forward.y * Random.Range(-0.3f, 0.3f)), firePoint.forward.z + (firePoint.forward.z * Random.Range(-0.1f, 0.1f))).normalized; }
}
