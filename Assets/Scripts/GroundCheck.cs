using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public CharacterController2D player2D;
    public ThirdPersonMovement player3D;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground") { if(player2D != null) { player2D.isGrounded = true; } if (player3D != null) { player3D.grounded = true; } }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground") { if (player2D != null) { player2D.isGrounded = false; } if (player3D != null) { player3D.grounded = false; } }
    }
}
