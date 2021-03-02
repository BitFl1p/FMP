﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
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

    void Update()
    {
        

    }
    private void LateUpdate()
    {
        if (start)
        {
            speed = speed * 100;
            transform.position = pos;
            GetComponent<Rigidbody>().velocity = direction * speed;

            transform.rotation = rotation;
            start = false;
        }
        lastPos = transform.position;
        /*Ray ray = new Ray(transform.position, lastPos);
        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo, speed))
        {
            Health health = hitInfo.collider.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(damage);
                Destroy(gameObject);
            }
            if (hitInfo.collider != null)
            {
                Destroy(gameObject);
            }
        }*/
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
