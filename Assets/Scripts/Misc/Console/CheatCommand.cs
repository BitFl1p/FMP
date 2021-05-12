using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Cheat Command", menuName = "Scripts/Misc/Cheat Command")]
public class CheatCommand : ConsoleCommand
{

    public override bool Process(string[] args)
    {
        DeveloperConsoleBehaviour devcon = FindObjectOfType<DeveloperConsoleBehaviour>();
        if (args.Length > 1) { devcon.Out("Invalid syntax, this command takes only one string value of either 'on' or 'off' (case sensitive)"); return false; }
        if(args.Length == 1)
        {
            if (args[0] == "on")
            {
                devcon.cheats = true;
                devcon.Out("Cheats have been enabled!");
            }
            else if (args[0] == "off")
            {
                devcon.cheats = false;
                devcon.Out("Cheats have been disabled!");
            }
            if (!(args[0] == "off" || args[0] == "on")) { devcon.Out("Invalid syntax, this command takes only one string value of either 'on' or 'off' (case sensitive)"); return false; }
        }
        else
        {
            if (devcon.cheats)
            {
                devcon.Out("Cheats are enabled!");
            }
            else
            {
                devcon.Out("Cheats are disabled!");
            }
        }
        
        return true;
    }
}