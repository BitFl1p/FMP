using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyExplodeBullet : EnemyProjectile
{
    public void SetData(int damage, float critChance, Quaternion rotation, Vector3 direction, float speed, Vector3 pos, EnemyExplode explode)
    {
        SetData(damage, critChance, rotation, direction, speed, pos);
        this.explode = explode;

    }
    internal EnemyExplode explode;

    internal override void Kill()
    {
        float rand = Random.Range(0, 100);
        if (rand <= critChance) Instantiate(explode).Wee(damage * 2, transform.position);
        else Instantiate(explode).Wee(damage, transform.position);
        Destroy(gameObject);
    }
}
