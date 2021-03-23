using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBullet : Projectile
{
    public void SetData(int damage, Quaternion rotation, Vector3 direction, float speed, Vector3 pos, Explode explode)
    {
        SetData(damage, rotation, direction, speed, pos);
        this.explode = explode;
    }
    public string axis;
    public Explode explode;



    private void LateUpdate()
    {
        transform.rotation.SetLookRotation(GetComponent<Rigidbody>().velocity);
        if (destroy)
        {
            Instantiate(explode).Wee(damage, transform.position);
            Destroy(gameObject);
        }
    }
    
}
