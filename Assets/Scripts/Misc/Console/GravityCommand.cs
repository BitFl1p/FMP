using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Gravity Command", menuName = "Scripts/Misc/Gravity Command")]
public class GravityCommand : ConsoleCommand
{

    public override bool Process(string[] args)
    {
        if (args.Length != 1) { return false; }
        return true;
    }
}
