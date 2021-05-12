using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stat Command", menuName = "Scripts/Misc/Stat Command")]
public class StatCommand : ConsoleCommand
{
    [SerializeField] string[] commands;
    public override bool Process(string[] args)
    {
        DeveloperConsoleBehaviour devcon = FindObjectOfType<DeveloperConsoleBehaviour>();
        if (args.Length != 2) { devcon.Out("Invalid syntax, this command takes no arguments"); return false; }
        foreach (string command in commands) devcon.Out(command);
        return true;
    }
}
