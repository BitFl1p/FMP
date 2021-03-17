using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public Gun2D gun2D;
    public ZapCannon zap;
    public Gun[] gun;
    public AutoGun sentry;
    public void GunDone()
    {
        foreach (Gun goon in gun)
        {
            goon.done = true;
        }

    }
    public void ZapDone()
    {
        zap.done = true;
    }
    public void SentryDone()
    {
        sentry.done = true;
    }
    public void SpawnSentry()
    {
        Instantiate(sentry).transform.position = new Vector3(transform.position.x, transform.position.y + 5, transform.position.z); ;
        
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    public void Gun2DDone()
    {
        gun2D.done = true;
    }
}
