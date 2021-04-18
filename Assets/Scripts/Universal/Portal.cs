using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject vCam3D, player3D, vCam2D, player2D, endPortal;
    public bool goes2D;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && goes2D)
        {
            player3D = other.gameObject;
            if (Input.GetKeyDown(KeyCode.E))
            {
                vCam2D.SetActive(true);
                player2D.SetActive(true);
                PlayerGunSelector gunSel2D = player2D.GetComponent<PlayerGunSelector>();
                PlayerGunSelector gunSel3D = player3D.GetComponent<PlayerGunSelector>();
                gunSel2D.ChangeWeapon(gunSel3D.getDamage(), gunSel3D.getWepNum());
                player2D.transform.position = endPortal.transform.position;
                player3D.SetActive(false);
                vCam3D.SetActive(false);
            }
        }

        else if (other.tag == "Player" && !goes2D)
        {
            player2D = other.gameObject;
            if (Input.GetKeyDown(KeyCode.V))
            {
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

}
