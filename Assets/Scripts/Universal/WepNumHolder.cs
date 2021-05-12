using UnityEngine;
using UnityEngine.InputSystem;

public class WepNumHolder : MonoBehaviour
{
    public int wepNum;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && InputSystem.GetDevice<Keyboard>().vKey.wasPressedThisFrame)
        {
            GetComponentInParent<GunShop>().player = other.gameObject.GetComponent<PlayerGunSelector>();
            GetComponentInParent<GunShop>().wepNum = wepNum;
            GetComponentInParent<GunShop>().purchase = true;
        }
    }
}
