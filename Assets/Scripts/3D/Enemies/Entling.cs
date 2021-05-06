using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entling : EnemyAI
{
    public Transform hip;
    public Vector3 offset;
    internal override void SetRB()
    {
        rb = transform.parent.Find("Sphere").GetComponent<Rigidbody>();
    }
    internal override void FixedUpdate()
    {
        base.FixedUpdate();
        transform.position = rb.transform.position + offset;
        hip.LookAt(target);
        hip.localEulerAngles += new Vector3(0, -90, -20);
        hip.localEulerAngles = new Vector3(0, hip.localEulerAngles.y, hip.localEulerAngles.z);
    }
}
