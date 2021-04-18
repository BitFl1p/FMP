using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public CharacterController2D player2D;
    public ThirdPersonMovement player3D;
    private void Update()
    {
        if (player2D != null) transform.position = player2D.transform.position; else transform.position = player3D.transform.position;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.isTrigger == false && other.gameObject.tag == "Ground") { if (player2D != null) { player2D.isGrounded = true; } if (player3D != null) { player3D.grounded = true; } }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger == false && other.gameObject.tag == "Ground") { if (player2D != null) { player2D.isGrounded = false; } if (player3D != null) { player3D.grounded = false; } }
    }
}
