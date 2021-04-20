using System.Collections.Generic;
using UnityEngine;

public class GunShop : MonoBehaviour
{
    public GameObject[] weapons;
    public List<GameObject> availableWeapons;
    public bool purchase;
    public int WaveNum;
    public PlayerGunSelector player;
    public int wepNum;
    private void OnEnable()
    {
        Restock();
    }
    private void Update()
    {
        if (purchase)
        {
            player.ChangeWeapon(WaveNum, wepNum);
            Restock();
            purchase = false;
        }
    }
    void Restock()
    {
        foreach (GameObject wep in availableWeapons) { Destroy(wep); }
        availableWeapons.Clear();
        availableWeapons.Add(Instantiate(weapons[Random.Range(0, weapons.Length)], transform));
        availableWeapons[0].transform.localPosition = new Vector3(-0.8f, 0, 0);
        availableWeapons.Add(Instantiate(weapons[Random.Range(0, weapons.Length)], transform));
        availableWeapons[1].transform.localPosition = new Vector3(0.8f, 0, 0);
    }
}
