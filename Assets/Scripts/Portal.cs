using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public GameObject vCam3D, player3D, vCam2D, player2D, endPortal, orb;
    public bool goes2D;
    bool orbActive;
    private void Update()
    {
        if (orbActive)
        {
            GameObject instance = Instantiate(orb);
            instance.transform.position = Vector3.Lerp(transform.position, endPortal.transform.position, 1);
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if(Input.GetKeyDown(KeyCode.E)&&goes2D)
        {
            vCam3D.SetActive(false);
            player3D.SetActive(false);
            
            orbActive = true;
            
        } 
        else if (Input.GetKeyDown(KeyCode.E)&&!goes2D)
        {
            vCam3D.SetActive(true);
            player3D.SetActive(true);
            player2D.SetActive(false);
            vCam2D.SetActive(false);
            player3D.transform.position = endPortal.transform.position;
            orbActive = true;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Orb")
        {
            if (goes2D)
            {
                
                player2D.SetActive(true);
                vCam2D.SetActive(true);
                player2D.transform.position = endPortal.transform.position;
                Destroy(other.gameObject);
            }
            else
            {

            }
        }
    }
}
