using System.Collections.Generic;
using UnityEngine;

public class PlayerGunSelector : MonoBehaviour
{
    public GameObject[] guns;
    int wepNum;
    float damage;
    public void ChangeWeapon(float damage, int wepNum)
    {
        this.wepNum = wepNum;
        this.damage = damage;
        foreach (GameObject gun in guns) { gun.SetActive(false); }
        if (guns[wepNum].GetComponent<GunBase>() != null) guns[wepNum].GetComponent<GunBase>().damageMultiplier = damage;
        guns[wepNum].SetActive(true);
    }
    public int getWepNum() { foreach (GameObject gun in guns) { if (gun.activeSelf) wepNum = gun.GetComponent<GunBase>().wepNum; } return wepNum; }
    public float getDamage() { foreach (GameObject gun in guns) { if (gun.activeSelf) damage = gun.GetComponent<GunBase>().damageMultiplier; } return damage; }
}
