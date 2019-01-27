using UnityEngine;
using System.Collections;



public class TimeCommand : Command
{
    private string command;
    private string[] args;

    public TimeCommand(string command, string[] args)
    {
        this.command = command;
        this.args = args;
    }

    public override Commands GetCommandType()
    {
        return Commands.time;
    }

    public override IEnumerator Execute(Game gameContext)
    {
        yield return gameContext.displayTextSlowlyMethod("Current time is " + Utilities.getFutureTime() + " \n", 0);
    }
}
