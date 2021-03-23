using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : Projectile
{
    
    public void SetData(int damage, Quaternion rotation, Vector3 direction, float speed, Vector3 pos, GameObject player)
    {
        SetData(damage, rotation, direction, speed, pos);
        this.player = player;
    }
    GameObject player;
    internal override void Aim()
    {
        GetComponent<Rigidbody>().velocity += player.transform.position - transform.position;
        GetComponent<Rigidbody>().velocity += new Vector3(0, speed / 1000, 0);
    }

}
