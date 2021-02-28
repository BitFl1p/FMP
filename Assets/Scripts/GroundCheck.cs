using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public CharacterController2D player;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ground")
        {
            player.isGrounded = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            player.isGrounded = false;
        }
    }
}
