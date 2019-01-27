using UnityEngine;
using System.Collections;



public class HelpCommand : Command
{


    public HelpCommand(string command, string[] args)
    {
        this.command = command;
        this.args = args;
    }

    public override Commands GetCommandType()
    {
        return Commands.help;
    }

    public override string getHelpMessage()
    {
        return "Use help alone to see available commands, use help [commandName] to see how to use a command\n";
    }
    public override IEnumerator Execute(Game gameContext)
    {
        const int nColCommands = 4;
        if (args == null)
        {
        
            string output = "Commands:\n";
            int nCommandLine = 0;
            foreach (Commands command in System.Enum.GetValues(typeof(Commands)))
            {
                if (command < Commands.invalid)
                {
                    output += command + " ";
                    nCommandLine++;
                    if (nCommandLine >= nColCommands)
                    {
                        output += "\n";
                        nCommandLine = 0;
                    }
                }
            }

            output += "\n";
            yield return gameContext.displayTextSlowlyMethod(output);
        }
        else
        {
            if (args.Length == 1)
            {
                string msg = Command.createCommand(args[0]).getHelpMessage();
                yield return gameContext.displayTextSlowlyMethod(msg);
            }
            else
            {
                yield return gameContext.displayTextSlowlyMethod("Help Command should be give 0 or 1 arguments\n");
            }
        }
    }
}
