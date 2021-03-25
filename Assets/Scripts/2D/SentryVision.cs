using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SentryVision : MonoBehaviour
{
    public bool isLeft;

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            if (isLeft) GetComponentInParent<AutoGun2D>().enemyLeft = true;
            else GetComponentInParent<AutoGun2D>().enemyRight = true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            if (isLeft) GetComponentInParent<AutoGun2D>().enemyLeft = false;
            else GetComponentInParent<AutoGun2D>().enemyRight = false;
        }
    }
}
