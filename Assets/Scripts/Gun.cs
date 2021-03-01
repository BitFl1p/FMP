using UnityEngine;

public class Gun : MonoBehaviour
{
    public ParticleSystem muzzle;
    public Transform bullet;
    public Transform firePoint;
    public float fireRate = 1;
    public int damage = 1;
    float timer;
    void Update()
    {
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
        //Debug.DrawRay(firePoint.position, firePoint.forward * 100, Color.red, 2f);
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hitInfo;
        if(muzzle != null) { muzzle.Play(); }
        if(bullet != null) { Transform bul = Instantiate(bullet).transform; bul.transform.position = firePoint.position; bul.transform.rotation = firePoint.rotation; }
        if (Physics.Raycast(ray, out hitInfo, 100))
        {
            Health health = hitInfo.collider.GetComponent<Health>();
            if(health != null)
            {
                health.TakeDamage(damage);
            }
        }
    }
}
