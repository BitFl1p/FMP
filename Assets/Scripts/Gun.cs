using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject bullet;
    public ParticleSystem muzzle;
    public float speed;
    public Transform firePoint;
    public float fireRate = 0;
    public int damage = 1;
    float timer;
    void Update()
    {
        //Time.timeScale = 0.1f;
        timer += Time.deltaTime;
        if(timer >= fireRate)
        {
            if (Input.GetButton("Fire1"))
            {
                timer = 0;
                FireGun();
            }
        }
    }
    void FireGun()
    {
        if (muzzle != null) { muzzle.Play(); }
        Instantiate(bullet).GetComponent<Bullet>().SetData(damage, firePoint.rotation, firePoint.forward, speed, firePoint.position);

        //Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.red, 2f);
        
    }
}
