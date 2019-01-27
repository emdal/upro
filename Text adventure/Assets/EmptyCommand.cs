using UnityEngine;
using System.Collections;



public class EmptyCommand: Command
{


    public EmptyCommand(string command, string[] args)
    {
        this.command = command;
        this.args = args;
    }

    public override Commands GetCommandType()
    {
        return Commands.empty;
    }

    public override IEnumerator Execute(Game gameContext)
    {
        yield return gameContext.displayTextSlowlyMethod("\n",0);        
    }
}
