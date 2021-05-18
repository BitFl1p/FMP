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
                CharacterController2D playerCon = player2D.GetComponent<CharacterController2D>();
                playerCon.lastMove = 1; 
                /*if (!playerCon.flip)
                {
                    if (axis == "ZY") player2D.transform.eulerAngles = new Vector3(180, 180, -180);
                    else player2D.transform.eulerAngles = new Vector3(180, 90, -180);
                }
                else
                {
                    if (axis == "ZY") player2D.transform.eulerAngles = new Vector3(180, 90, -180);
                    else player2D.transform.eulerAngles = new Vector3(180, 0, -180);
                }*/
                SetStats(player3D.GetComponent<Stats>(), player2D.GetComponent<Stats>());
                player2D.SetActive(true);
                PlayerGunSelector gunSel2D = player2D.GetComponent<PlayerGunSelector>();
                PlayerGunSelector gunSel3D = player3D.GetComponent<PlayerGunSelector>();
                gunSel2D.ChangeWeapon(gunSel3D.getDamage(), gunSel3D.getWepNum());
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
                SetStats(player2D.GetComponent<Stats>(), player3D.GetComponent<Stats>());
                vCam3D.SetActive(true);
                player3D.SetActive(true);
                PlayerGunSelector gunSel2D = player2D.GetComponent<PlayerGunSelector>();
                PlayerGunSelector gunSel3D = player3D.GetComponent<PlayerGunSelector>();
                gunSel3D.ChangeWeapon(gunSel2D.getDamage(), gunSel2D.getWepNum());
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
