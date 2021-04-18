using UnityEngine;

public class WepNumHolder : MonoBehaviour
{
    public int wepNum;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && Input.GetKeyDown(KeyCode.V))
        {
            GetComponentInParent<GunShop>().player = other.gameObject.GetComponent<PlayerGunSelector>();
            GetComponentInParent<GunShop>().wepNum = wepNum;
            GetComponentInParent<GunShop>().purchase = true;
        }
    }
}
