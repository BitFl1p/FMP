using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateSpeen : MonoBehaviour
{
    Animator anim;
    float count, animTime;
    private void OnEnable()
    {
        anim = GetComponent<Animator>();
        count = Random.Range(0f, 3f);
    }
    private void FixedUpdate()
    {
        if(count <= 0)
        {
            if (Random.Range(1, 3) == 1)
            {
                anim.SetBool("Speen", true);
            }
            else
            {
                anim.SetBool("Speen2", true);
            }
            animTime = 1;
            count = Random.Range(0f, 3f);
        }
        else
        {
            count -= Time.deltaTime;
        }
        if(animTime <= 0)
        {
            anim.SetBool("Speen", false); 
            anim.SetBool("Speen2", false);
        }
        else
        {
            animTime -= Time.deltaTime;
        }
    }

}
