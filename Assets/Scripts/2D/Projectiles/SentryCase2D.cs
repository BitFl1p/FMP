using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryCase2D : Projectile2D
{

    public AutoGun2D sentry;
    private void OnEnable()
    {
        AutoGun2D[] sentries = FindObjectsOfType<AutoGun2D>();
        if (sentries.Length > 2) for (int i = 0; i < sentries.Length; i++) if (i > 2) sentries[i].Explode();
    }
    internal override void Kill()
    {
        //GetComponent<Rigidbody>().velocity = new Vector3(0, 1, 0);
        GetComponent<Rigidbody>().isKinematic = true;
        //transform.position = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        GetComponent<Animator>().SetBool("Dying", true);
    }
    internal override void Aim()
    {
        if (axis != "XY") transform.eulerAngles = new Vector3(0, 90, 0);
    }

}