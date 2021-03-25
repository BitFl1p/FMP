using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeBullet2D : Projectile2D
{
    public void SetData(int damage, Quaternion rotation, Vector3 direction, float speed, Vector3 pos, string axis, Explode2D explode)
    {
        SetData(damage, rotation, direction, speed, pos, axis);
        this.explode = explode;

    }
    internal Explode2D explode;

    internal override void Kill()
    {
        if(axis == "XY") Instantiate(explode).Wee(damage, transform.position, Vector3.zero);
        else Instantiate(explode).Wee(damage, transform.position, new Vector3(0,90,0));
        Destroy(gameObject);
    }
}
