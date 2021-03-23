using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    public void SetData(int damage, Quaternion rotation, Vector3 direction, float speed, Vector3 pos, GameObject player)
    {
        this.damage = damage;
        this.rotation = rotation;
        this.direction = direction;
        this.speed = speed;
        this.pos = pos;
        this.player = player;
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
    
    private void LateUpdate()
    {
        Debug.Log("yaet");
        if (start)
        {
            
            speed = speed * 100;
            transform.position = pos;
            GetComponent<Rigidbody>().velocity = direction * speed;
            start = false;
            lastPos = transform.position;
        }
        else
        {
            
            GetComponent<Rigidbody>().velocity += player.transform.position - transform.position;
            GetComponent<Rigidbody>().velocity += new Vector3(0, speed / 1000, 0);

            Ray ray = new Ray(transform.position, lastPos);
            RaycastHit info;
            if (Physics.Raycast(ray, out info, 100))
            {
                Health health = info.collider.GetComponent<Health>();
                if (health != null)
                {
                    health.TakeDamage(damage);
                }
                Destroy(gameObject);
            }
            lastPos = transform.position;
        }
        
    }
    

}
