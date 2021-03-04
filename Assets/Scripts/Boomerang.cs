using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public void SetData(int damage, Quaternion rotation, Vector3 direction, float speed, Vector3 pos)
    {
        this.damage = damage;
        this.rotation = rotation;
        this.direction = direction;
        this.speed = speed;
        this.pos = pos;
        start = true;
    }

    public int damage;
    public Quaternion rotation;
    public Vector3 direction;
    public float speed;
    public Vector3 pos;
    public Vector3 lastPos;
    bool start;
    GameObject player;
    private void Awake()
    {
        player = FindObjectOfType<ThirdPersonMovement>().gameObject;
    }
    private void FixedUpdate()
    {
        Ray ray = new Ray(transform.position, lastPos);
        RaycastHit info;
        if(Physics.Raycast(ray, out info, 100))
        {
            Health health = info.collider.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
    private void LateUpdate()
    {
        lastPos = transform.position;
        transform.eulerAngles = new Vector3(90, transform.eulerAngles.y, transform.eulerAngles.z);
        transform.eulerAngles += new Vector3(0,-30,0);
        GetComponent<Rigidbody>().velocity += player.transform.position -transform.position;
        GetComponent<Rigidbody>().velocity += new Vector3(0, speed / 1000, 0);
        
        if (start)
        {
            speed = speed * 100;
            transform.position = pos;
            GetComponent<Rigidbody>().velocity = direction * speed;

            transform.rotation = rotation;
            start = false;
        }
        lastPos = transform.position;

    }
    private void OnCollisionEnter(Collision other)
    {

        Health health = other.collider.GetComponent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

}
