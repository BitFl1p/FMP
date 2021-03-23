using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBullet : MonoBehaviour
{
    public void SetData(int damage, Quaternion rotation, Vector3 direction, float speed, Vector3 pos, GameObject explode, string axis)
    {

        this.explode = explode.GetComponent<Explode>();
        this.damage = damage;
        this.rotation = rotation;
        this.direction = direction;
        this.speed = speed;
        this.pos = pos;
        if(axis != null) this.axis = axis;
        start = true;
    }
    public string axis;
    public Explode explode;
    public Explode2D explode2D;
    public int damage;
    public Quaternion rotation;
    public Vector3 direction;
    public float speed;
    public Vector3 pos;
    public Vector3 lastPos;
    bool start;


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
        RaycastHit[] hits = Physics.RaycastAll(new Ray(lastPos,(transform.position - lastPos).normalized), (transform.position - lastPos).magnitude);
        foreach (RaycastHit hit in hits)
        {
            if(hit.collider.isTrigger == false && hit.collider.gameObject.tag != "OuterWall")
            {
                if(explode != null) Instantiate(explode).Wee(damage, transform.position);
                else if (explode2D != null) Instantiate(explode2D)?.Wee(damage, transform.position);
                Destroy(gameObject);
                
            }
        }
        
        lastPos = transform.position;
        transform.rotation.SetLookRotation(GetComponent<Rigidbody>().velocity);
        if(axis == "ZY")transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y+90, transform.rotation.z);
        
    }
    
}
