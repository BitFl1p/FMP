using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth2D : EnemyHealth
{
    public string axis = "XY";
    internal override void Aim()
    {
        axis = GetComponent<EnemyAI2D>().axis;
        if (axis == "XY") healthCanvas.eulerAngles = new Vector3(0, 0, 0);
        else healthCanvas.eulerAngles = new Vector3(0, 90, 0);
    }
}
