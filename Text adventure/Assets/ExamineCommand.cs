using UnityEngine;
using System.Collections;



public class ExamineCommand: Command
{


    public ExamineCommand(string command, string[] args)
    {
        this.command = command;
        this.args = args;
    }

    public override Commands GetCommandType()
    {
        return Commands.examine;
    }
    public override string getHelpMessage()
    {
        return "lol plzz add something here \n";
    }
    public override IEnumerator Execute(Game gameContext)
    {
        Debug.Log(gameContext.currentTerminal);
        string message = gameContext.currentTerminal.getLocation().examine();

        foreach(Entity e in gameContext.currentTerminal.getLocation().containedItems)
        {
            if (args != null && args[0].Equals(e.name))
            {
                message = e.examine();
            }
        }

        yield return gameContext.displayTextSlowlyMethod(message + "\n",0);        
    }
}
