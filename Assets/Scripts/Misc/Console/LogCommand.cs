using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Log Command", menuName = "Scripts/Misc/Log Command")]
public class LogCommand : ConsoleCommand
{
    
    public override bool Process(string[] args)
    {
        string logText = string.Join(" ", args);
        FindObjectOfType<DeveloperConsoleBehaviour>().Out(logText);
        return true;
    }
}
