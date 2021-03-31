using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBullet2D : Projectile2D
{
    public void SetData(float damage, float critChance, Quaternion rotation, Vector3 direction, float speed, Vector3 pos, string axis, Explode2D explode)
    {
        SetData(damage, critChance, rotation, direction, speed, pos, axis);
        this.explode = explode;

    }
    internal Explode2D explode;

    internal override void Kill()
    {
        if(axis == "XY") if (Random.Range(0, 100) <= critChance) Instantiate(explode).Wee(damage * 2, transform.position, Vector3.zero); else Instantiate(explode).Wee(damage * 2, transform.position, Vector3.zero);
        else if (Random.Range(0, 100) <= critChance) Instantiate(explode).Wee(damage*2, transform.position, new Vector3(0, 90, 0)); else Instantiate(explode).Wee(damage * 2, transform.position, new Vector3(0, 90, 0));
        Destroy(gameObject);
    }
}
