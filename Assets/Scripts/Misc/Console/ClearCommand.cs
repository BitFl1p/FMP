using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Clear Command", menuName = "Scripts/Misc/Clear Command")]
public class ClearCommand : ConsoleCommand
{
    public override bool Process(string[] args)
    {
        DeveloperConsoleBehaviour devcon = FindObjectOfType<DeveloperConsoleBehaviour>();
        if (args.Length != 0) { devcon.Out("Invalid syntax, this command takes no arguments"); return false; }
        devcon.Clear();
        return true;
    }
}
