using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBall : MonoBehaviour
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
    int count = 0;
    public int damage;
    public Quaternion rotation;
    public Vector3 direction;
    public float speed;
    public Vector3 pos;
    public Vector3 lastPos;
    bool start;
    public Explode explode;

    private void LateUpdate()
    {


        if (start)
        {
            speed = speed * 100;
            transform.position = pos;


            transform.rotation = rotation;
            start = false;
        }
        transform.position += direction * speed;
        lastPos = transform.position;
        transform.rotation.SetLookRotation(direction);
        if (count >= 3) { GetComponent<Die>().Deth(); }
    }
    private void OnTriggerEnter(Collider other)
    {
        Health health = other.GetComponent<Health>();
        health?.TakeDamage(damage);
        count++;
    }
    
    
}
