using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBullet : Projectile
{
    public void SetData(float damage, float critChance, Quaternion rotation, Vector3 direction, float speed, Vector3 pos, Explode explode)
    {
        SetData(damage, critChance, rotation, direction, speed, pos);
        this.explode = explode;
        
    }
    internal Explode explode;

    internal override void Kill()
    {
        float rand = Random.Range(0, 100);
        if (rand <= critChance) Instantiate(explode).Wee(damage * 2, transform.position);
        else Instantiate(explode).Wee(damage, transform.position);
        Destroy(gameObject);
    }
    
    
}
