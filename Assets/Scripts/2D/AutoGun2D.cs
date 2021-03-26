using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoGun2D : MonoBehaviour
{
    public string axis;
    public Vector3 leftVector, rightVector;
    public bool enemyLeft, enemyRight;
    public Transform head;
    public Bullet2D bullet;
    public float speed;
    public Transform firePoint;
    public bool done = true;
    public int damage = 1;
    public Animator anim;
    public Explode2D explosion;
    bool detected;

    private void OnEnable()
    {
        AutoGun2D[] sentries = FindObjectsOfType<AutoGun2D>();
        if (axis == "XY")
        {
            transform.eulerAngles = new Vector3(0, 90, 0);
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionZ;
        }
        else
        {
            transform.eulerAngles = Vector3.zero;
            GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionX;
        }
        
        
    }
    private void Update()
    {
        if (done) anim.SetBool("Shoot", false); //anim.enabled = false;
        if (enemyLeft && done)
        {
            head.eulerAngles = leftVector;
            anim.SetBool("Shoot", false);
            done = false;
            Shoot();
        }
        else if (enemyRight && done)
        {
            head.eulerAngles = rightVector;
            anim.SetBool("Shoot", false);
            done = false;
            Shoot();
        }
    }
    void Shoot()
    {

        Instantiate(bullet).SetData(damage, firePoint.rotation, firePoint.forward, speed, firePoint.position, axis);
        anim.SetBool("Shoot", true);

    }


    public void Explode()
    {
        if (axis == "XY") Instantiate(explosion).Wee(damage, transform.position, new Vector3(0,0,0));
        else Instantiate(explosion).Wee(damage, transform.position, new Vector3(0, 90, 0));
        Destroy(gameObject);
    }
}
