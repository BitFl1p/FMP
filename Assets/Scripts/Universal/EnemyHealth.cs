using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
public class EnemyHealth : Health
{
    public RectTransform healthCanvas;
    Transform player;
    internal override void OnEnable()
    {
        base.OnEnable();
        player = FindObjectOfType<ThirdPersonMovement>().gameObject.transform;
    }
    internal override void Update()
    {
        base.Update();
        healthCanvas.LookAt(player);
        if (currentHealth == maxHealth) healthCanvas.gameObject.SetActive(false);
        else healthCanvas.gameObject.SetActive(true);

    }
}
