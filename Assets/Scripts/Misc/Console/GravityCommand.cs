using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Gravity Command", menuName = "Scripts/Misc/Gravity Command")]
public class GravityCommand : ConsoleCommand
{
    
    public override bool Process(string[] args)
    {
        DeveloperConsoleBehaviour devcon = FindObjectOfType<DeveloperConsoleBehaviour>();
        if (!devcon.cheats) { devcon.Out("Cheats are disabled, use 'cheats on' to enable them. (this will also disable any rewards or achievements you get)"); return false; }
        if (args.Length != 3 && args.Length != 0) { devcon.Out("Invalid syntax, this command requires 3 float values"); return false; }
        if (args.Length == 3)
        {
            Vector3 newGrav = Vector3.zero;
            float count = 0;
            foreach (string arg in args)
            {

                if (!float.TryParse(arg, out float value))
                {
                    devcon.Out("Invalid syntax, this command requires 3 numbers (decimals allowed)");
                    return false;
                }
                else
                {
                    switch (count)
                    {
                        case 0: newGrav.x = value; break;
                        case 1: newGrav.y = value; break;
                        case 2: newGrav.z = value; break;
                    }
                }
                count++;
            }
            Physics.gravity = newGrav;
            devcon.Out("Gravity set to: " + newGrav);
        }
        else
        {
            devcon.Out("Gravity is: " + Physics.gravity);
        }
        return true;
    }
}
