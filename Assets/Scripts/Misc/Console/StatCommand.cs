using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stat Command", menuName = "Scripts/Misc/Stat Command")]
public class StatCommand : ConsoleCommand
{
    public override bool Process(string[] args)
    {
        DeveloperConsoleBehaviour devcon = FindObjectOfType<DeveloperConsoleBehaviour>();
        if (!devcon.cheats) { devcon.Out("Cheats are disabled, use 'cheats on' to enable them. (this will also disable any rewards or achievements you get)"); return false; }
        if (args.Length != 2) { devcon.Out("Invalid syntax, this command takes 2 arguments"); return false; }
        switch (args[0])
        {
            case "regen":
                if (!float.TryParse(args[1], out float value1))
                {
                    devcon.Out("Invalid syntax, the regen value requires a number (decimals allowed)");
                    return false;
                }
                foreach(Stats stat in Resources.FindObjectsOfTypeAll(typeof(Stats)))
                {
                    stat.baseRegen = value1;
                }
                devcon.Out("Regen has been changed to " + value1);
                return true;
            case "maxhealth":
                if (!float.TryParse(args[1], out float value2))
                {
                    devcon.Out("Invalid syntax, the maxhealth value requires a number (decimals allowed)");
                    return false;
                }
                foreach (Stats stat in Resources.FindObjectsOfTypeAll(typeof(Stats)))
                {
                    stat.maxHealth = value2;
                }
                devcon.Out("Max health has been changed to " + value2);
                return true;
            case "damage":
                if (!int.TryParse(args[1], out int value3))
                {
                    devcon.Out("Invalid syntax, the damage value requires a number (decimals not allowed)");
                    return false;
                }
                foreach (Stats stat in Resources.FindObjectsOfTypeAll(typeof(Stats)))
                {
                    stat.baseDamage = value3;
                }
                devcon.Out("Damage has been changed to " + value3);
                return true;
            case "jumpheight":
                if (!float.TryParse(args[1], out float value4))
                {
                    devcon.Out("Invalid syntax, the jumpheight value requires a number (decimals allowed)");
                    return false;
                }
                foreach (Stats stat in Resources.FindObjectsOfTypeAll(typeof(Stats)))
                {
                    stat.jumpHeight = value4;
                }
                devcon.Out("Jump height has been changed to " + value4);
                return true;
            case "speed":
                if (!float.TryParse(args[1], out float value5))
                {
                    devcon.Out("Invalid syntax, the speed value requires a number (decimals allowed)");
                    return false;
                }
                foreach (Stats stat in Resources.FindObjectsOfTypeAll(typeof(Stats)))
                {
                    stat.moveSpeed = value5;
                }
                devcon.Out("Speed has been changed to " + value5);
                return true;
            case "jumpcount":
                if (!int.TryParse(args[1], out int value6))
                {
                    devcon.Out("Invalid syntax, the jumpcount value requires a number (decimals not allowed)");
                    return false;
                }
                foreach (Stats stat in Resources.FindObjectsOfTypeAll(typeof(Stats)))
                {
                    stat.jumpAmount = value6;
                }
                devcon.Out("Jump count has been changed to " + value6);
                return true;
            case "crit":
                if (!float.TryParse(args[1], out float value7))
                {
                    devcon.Out("Invalid syntax, the crit value requires a number (decimals allowed)");
                    return false;
                }
                foreach (Stats stat in Resources.FindObjectsOfTypeAll(typeof(Stats)))
                {
                    stat.critChance = value7;
                }
                devcon.Out("Crit chance has been changed to " + value7);
                return true;
            case "coins2d":
                if (!int.TryParse(args[1], out int value8))
                {
                    devcon.Out("Invalid syntax, the coins2d value requires a number (decimals not allowed)");
                    return false;
                }
                foreach (Stats stat in Resources.FindObjectsOfTypeAll(typeof(Stats)))
                {
                    stat.Coins2D = value8;
                }
                devcon.Out("2D Coins have been changed to " + value8);
                return true;
            case "coins3d":
                if (!int.TryParse(args[1], out int value9))
                {
                    devcon.Out("Invalid syntax, the coins3d value requires a number (decimals not allowed)");
                    return false;
                }
                foreach (Stats stat in Resources.FindObjectsOfTypeAll(typeof(Stats)))
                {
                    stat.Coins3D = value9;
                }
                devcon.Out("3D Coins have been changed to " + value9);
                return true;
            default:
                devcon.Out("Invalid syntax, stat " + args[0] + " not found");
                return false;
        }
    }
}
