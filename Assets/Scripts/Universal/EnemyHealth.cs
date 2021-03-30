using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
public class EnemyHealth : Health
{
    public RectTransform healthCanvas;
    Transform player;
    private void OnEnable()
    {
        player = FindObjectOfType<ThirdPersonMovement>().gameObject.transform;
    }
    private void Update()
    {
        healthCanvas.LookAt(player);
    }
}
