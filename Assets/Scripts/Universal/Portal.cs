using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject vCam3D, player3D, vCam2D, player2D, endPortal;
    public bool goes2D;
    
    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E) && other.tag == "Player" && goes2D)
        {

            vCam2D.SetActive(true);
            player2D.SetActive(true);
            player2D.transform.position = endPortal.transform.position;
            player3D.SetActive(false);
            vCam3D.SetActive(false);
        }

        else if (Input.GetKeyDown(KeyCode.V) && other.tag == "Player2D" && !goes2D)
        {
            vCam3D.SetActive(true);
            player3D.SetActive(true);
            player3D.transform.position = endPortal.transform.position;
            player2D.SetActive(false);
            vCam2D.SetActive(false);
        }
        
        
    }
    
}
