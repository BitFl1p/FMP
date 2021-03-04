using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    public Gun[] gun;
    public void GunDone()
    {
        foreach(Gun goon in gun)
        {
            goon.done = true;
        }
        
    }
}
