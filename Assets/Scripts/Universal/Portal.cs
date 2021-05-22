using UnityEngine;
using UnityEngine.InputSystem;

public class Portal : MonoBehaviour
{
    public GameObject vCam3D, player3D, vCam2D, player2D, endPortal;
    public bool goes2D;
    public string axis = "XY";

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && goes2D)
        {
            player3D = other.gameObject;
            if (InputSystem.GetDevice<Keyboard>().eKey.wasPressedThisFrame)
            {
                foreach (GunBase gun in FindObjectsOfType<GunBase>()) gun.done = true;
                CharacterController2D playerCon = player2D.GetComponent<CharacterController2D>();
                playerCon.lastMove = 1; 
                SetStats(player3D.GetComponent<Stats>(), player2D.GetComponent<Stats>());
                player2D.SetActive(true);
                PlayerGunSelector gunSel2D = player2D.GetComponent<PlayerGunSelector>();
                PlayerGunSelector gunSel3D = player3D.GetComponent<PlayerGunSelector>();
                gunSel2D.ChangeWeapon(gunSel3D.damage, gunSel3D.wepNum);
                player2D.transform.position = endPortal.transform.position;
                player3D.SetActive(false);
                vCam3D.SetActive(false);
                vCam2D.SetActive(true);
            }
        }

        else if (other.tag == "Player" && !goes2D)
        {
            player2D = other.gameObject;
            if (InputSystem.GetDevice<Keyboard>().vKey.wasPressedThisFrame)
            {
                foreach (GunBase gun in FindObjectsOfType<GunBase>()) gun.done = true;
                SetStats(player2D.GetComponent<Stats>(), player3D.GetComponent<Stats>());
                vCam3D.SetActive(true);
                player3D.SetActive(true);
                PlayerGunSelector gunSel2D = player2D.GetComponent<PlayerGunSelector>();
                PlayerGunSelector gunSel3D = player3D.GetComponent<PlayerGunSelector>();
                gunSel3D.ChangeWeapon(gunSel2D.damage, gunSel2D.wepNum);
                player3D.transform.position = endPortal.transform.position;
                player2D.SetActive(false);
                vCam2D.SetActive(false);
            }
            
        }


    }
    void SetStats(Stats toGet, Stats toSet)
    {
        toSet.baseRegen = toGet.baseRegen;
        toSet.maxHealth = toGet.maxHealth;
        toSet.baseDamage = toGet.baseDamage;
        toSet.jumpHeight = toGet.jumpHeight;
        toSet.moveSpeed = toGet.moveSpeed;
        toSet.jumpAmount = toGet.jumpAmount;
        toSet.critChance = toGet.critChance;
        toSet.Coins2D = toGet.Coins2D;
        toSet.Coins3D = toGet.Coins3D;
    }

}
