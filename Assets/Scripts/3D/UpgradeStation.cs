using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeStation : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            Stats playerStats = other.gameObject.GetComponent<Stats>();
            switch (Random.Range(1,9))
            {
                case 1: playerStats.baseRegen += Mathf.Ceil(playerStats.baseRegen / 4);  break;
                case 2: playerStats.maxHealth += Mathf.Ceil(playerStats.maxHealth / 4); break;
                case 3: playerStats.baseDamage += Mathf.Ceil(playerStats.baseDamage / 4); break;
                case 4: playerStats.baseRegen += Mathf.Ceil(playerStats.baseRegen / 4); break;
                case 5: playerStats.baseRegen += Mathf.Ceil(playerStats.baseRegen / 4); break;
                case 6: playerStats.baseRegen += Mathf.Ceil(playerStats.baseRegen / 4); break;
                case 7: playerStats.baseRegen += Mathf.Ceil(playerStats.baseRegen / 4); break;
                case 8: playerStats.baseRegen += Mathf.Ceil(playerStats.baseRegen / 4); break;
            }
        }
    }
}
