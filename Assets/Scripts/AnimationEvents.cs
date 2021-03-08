using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public ZapCannon zap;
    public Gun[] gun;
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
}
