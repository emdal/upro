using UnityEngine;
using System.Collections;



public class InvalidCommand : Command
{


    public InvalidCommand(string command, string[] args)
    {
        this.command = command;
        this.args = args;
    }

    public override string getHelpMessage()
    {
        return "Unknown Command \n";
    }

    public override Commands GetCommandType()
    {
        return Commands.invalid;
    }

    public override IEnumerator Execute(Game gameContext)
    {
        
            yield return gameContext.displayTextSlowlyMethod("This command does not exist, try using help to see available commands\n");
    }
}
