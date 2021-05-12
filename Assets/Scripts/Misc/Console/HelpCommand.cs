using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Help Command", menuName = "Scripts/Misc/Help Command")]
public class HelpCommand : ConsoleCommand
{
    [SerializeField] string[] commands;
    public override bool Process(string[] args)
    {
        DeveloperConsoleBehaviour devcon = FindObjectOfType<DeveloperConsoleBehaviour>();
        if (args.Length != 0) { devcon.Out("Invalid syntax, this command takes no arguments"); return false; }
        foreach (string command in commands) devcon.Out(command);
        return true;
    }
}
