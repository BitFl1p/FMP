using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Ent : EnemyAI
{
    public Transform hip;
    public Vector3 offset;
    internal override void FixedUpdate()
    {
        base.FixedUpdate();
        transform.position = rb.transform.position + offset;
        hip.LookAt(target);
        hip.localEulerAngles += new Vector3(0, -90, -20);
        hip.localEulerAngles = new Vector3(0, hip.localEulerAngles.y, hip.localEulerAngles.z);
    }

}
