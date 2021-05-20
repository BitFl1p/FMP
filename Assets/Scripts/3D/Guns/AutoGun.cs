using UnityEngine;

public class AutoGun : MonoBehaviour
{
    public float critChance;
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
        Camera.main.gameObject.GetComponentInParent<AudioManager>().sfx[0].Play();
        muzzle?.Play();
        Instantiate(bullet).GetComponent<Bullet>().SetData(damage, critChance, firePoint.rotation, firePoint.forward, speed, firePoint.position);
        anim.SetBool("Shoot", true);

    }
    private void OnEnable()
    {
        AutoGun[] sentries = FindObjectsOfType<AutoGun>();
        if (sentries.Length > 2) for (int i = 0; i < sentries.Length; i++) if (i > 2) sentries[i].Explode();
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyHealth>() != null && other.gameObject.layer == 10)
        {

            head.LookAt(FindObjectOfType<EnemyHealth>().transform.position);
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