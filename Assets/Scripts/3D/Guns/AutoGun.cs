using UnityEngine;
using System.Collections.Generic;
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
    List<Transform> enemies = new List<Transform> { };


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
    Transform GetClosestEnemy(List<Transform> enemies)
    {
        Transform tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (Transform t in enemies)
        {
            if (t)
            {
                float dist = Vector3.Distance(t.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
            
        }
        return tMin;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyHealth>() != null && other.gameObject.layer == 10)
        {
            if(!enemies.Contains(other.transform)) enemies.Add(other.transform);
            enemies.RemoveAll(item => item == null);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<EnemyHealth>() != null && other.gameObject.layer == 10)
        {
            if(enemies.Contains(other.transform)) enemies.Remove(other.transform);
            enemies.RemoveAll(item => item == null);
        }
    }
    private void FixedUpdate()
    {
        if (enemies != null)
        {
            head.LookAt(GetClosestEnemy(enemies));
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