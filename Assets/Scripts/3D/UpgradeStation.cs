using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UpgradeStation : MonoBehaviour
{
    public Transform[] positions;
    public int[] upNums;
    public GameObject[] upgradePrefabs;
    public List<GameObject> upgrades;
    public Animator anim;
    public Collider player;
    public bool playerHere;
    public int price;
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other;
            playerHere = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (GameObject upgrade in upgrades) upgrade.SetActive(false);
            anim.SetBool("Here", false);
            player = other;
            playerHere = false;
        }
    }
    private void OnEnable()
    {
        Reset();
    }
    private void Update()
    {
        if (playerHere)
        {
            anim.SetBool("Here", true);
            foreach (GameObject upgrade in upgrades) upgrade.SetActive(true);
            if (InputSystem.GetDevice<Keyboard>().eKey.wasPressedThisFrame)
            {
                if (player.GetComponent<Stats>().Coins3D < price) return;
                Stats playerStats = player.GetComponent<Stats>();
                RaycastHit hit;

                int layerMask = LayerMask.GetMask("Player", "Obstacle");
                layerMask = ~layerMask;
                if (Physics.Raycast(Camera.main.gameObject.transform.position, Camera.main.gameObject.transform.forward, out hit, Mathf.Infinity, layerMask))
                {
                    if (hit.collider.gameObject.GetComponent<UpgradeHolder>() != null)
                    {
                        switch (hit.collider.gameObject.GetComponent<UpgradeHolder>().upgradeNum)
                        {
                            case 1: playerStats.baseRegen += Mathf.Ceil(playerStats.baseRegen / 4); break;
                            case 2: playerStats.maxHealth += Mathf.Ceil(playerStats.maxHealth / 4); break;
                            case 3: playerStats.baseDamage += (int)Mathf.Ceil(playerStats.baseDamage / 10f); break;
                            case 4: playerStats.jumpHeight += 0.2f; break;
                            case 5: playerStats.moveSpeed += 0.5f; break;
                            case 6: playerStats.jumpAmount += 1; break;
                            case 7: playerStats.critChance += 2; break;
                        }
                        player.GetComponent<Stats>().Coins3D -= price;
                        price += (int)(price * 0.25);
                        foreach(UpgradeStation shop in FindObjectsOfType<UpgradeStation>())
                        {
                            shop.price = price;
                        }
                        Reset();
                    }
                }
            }
        }
        
    }
    public void Reset()
    {
        foreach (GameObject upgrade in upgrades) Destroy(upgrade);
        upgrades.Clear();
        for (int i = 1; i <= 3; i++) upgrades.Add(Instantiate(upgradePrefabs[Random.Range(0, upgradePrefabs.Length)]));
        upgrades[0].transform.position = positions[0].position;
        upgrades[0].transform.SetAsFirstSibling();
        upgrades[1].transform.position = positions[1].position;
        upgrades[1].transform.SetAsFirstSibling();
        upgrades[2].transform.position = positions[2].position;
        upgrades[2].transform.SetAsFirstSibling();
        foreach (GameObject upgrade in upgrades) upgrade.SetActive(false);
    }


    
}
