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
        foreach(string command in commands) devcon.Out(command);
        return true;
    }
}
