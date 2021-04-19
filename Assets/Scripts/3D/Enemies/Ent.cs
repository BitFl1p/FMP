using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Ent : EnemyAI
{
    public Vector3 offset;
    private void FixedUpdate()
    {
        transform.position = rb.transform.position + offset;
    }

}
