using System.Collections.Generic;
using UnityEngine;

public class PlayerGunSelector : MonoBehaviour
{
    public GameObject[] guns;
    public int wepNum;
    public float damage;
    public void ChangeWeapon(float damage, int wepNum)
    {
        this.wepNum = wepNum;
        this.damage = damage;
        foreach (GameObject gun in guns) { gun.SetActive(false); }
        if (guns[wepNum].GetComponent<GunBase>() != null) guns[wepNum].GetComponent<GunBase>().damageMultiplier = damage;
        guns[wepNum].SetActive(true);
    }
}
