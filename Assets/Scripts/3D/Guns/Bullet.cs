using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{   
    private void LateUpdate()
    {
        if (destroy) Destroy(gameObject); 
    }

}
