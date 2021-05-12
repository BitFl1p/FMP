using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New God Command", menuName = "Scripts/Misc/God Command")]
public class GodCommand : ConsoleCommand
{

    public override bool Process(string[] args)
    {
        DeveloperConsoleBehaviour devcon = FindObjectOfType<DeveloperConsoleBehaviour>();
        if (!devcon.cheats) { devcon.Out("Cheats are disabled, use 'cheats on' to enable them. (this will also disable any rewards or achievements you get)"); return false; }
        if (args.Length > 1) { devcon.Out("Invalid syntax, this command takes only one string value of either 'on' or 'off' (case sensitive)"); return false; }
        if (args.Length == 1)
        {
            if (args[0] == "on")
            {
                foreach (PlayerHealth player in Resources.FindObjectsOfTypeAll(typeof(PlayerHealth)))
                {
                    player.godMode = true;
                }
                devcon.god = true;
                devcon.Out("GodMode has been enabled!");
            }
            else if (args[0] == "off")
            {
                foreach (PlayerHealth player in Resources.FindObjectsOfTypeAll(typeof(PlayerHealth)))
                {
                    player.godMode = false;
                }
                devcon.god = false;
                devcon.Out("GodMode has been disabled!");
            }
            if (!(args[0] == "off" || args[0] == "on")) { devcon.Out("Invalid syntax, this command takes only one string value of either 'on' or 'off' (case sensitive)"); return false; }
        }
        else
        {
            if (devcon.god)
            {
                devcon.Out("GodMode is enabled!");
            }
            else
            {
                devcon.Out("GodMode are disabled!");
            }
        }
        return true;
    }
}
