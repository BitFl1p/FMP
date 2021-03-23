using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang2D : Projectile
{
    public void SetData(int damage, Quaternion rotation, Vector3 direction, float speed, Vector3 pos, GameObject player)
    {
        SetData(damage, rotation, direction, speed, pos);
        this.player = player;
    }
    GameObject player;
    private void LateUpdate() { Shoot(); }
}
