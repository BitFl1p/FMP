using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GwallCheck : MonoBehaviour
{
    public EnemyAI2D guy;
    public bool wall, ground;
    private void OnTriggerEnter(Collider other)
    {
        if (wall && other.gameObject.tag == "Ground") guy.walled = true;
        else if (ground && other.gameObject.tag == "Ground") guy.isGrounded = true;
    }
    private void OnTriggerExit(Collider other)
    {
        if (wall && other.gameObject.tag == "Ground") guy.walled = false;
        else if (ground && other.gameObject.tag == "Ground") guy.isGrounded = false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (wall && other.gameObject.tag == "Ground") guy.walled = true;
        else if (ground && other.gameObject.tag == "Ground") guy.isGrounded = true;
    }
}
