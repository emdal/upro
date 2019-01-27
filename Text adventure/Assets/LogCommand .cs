using UnityEngine;
using System.Collections;



public class LogCommand : Command
{

    private static LogArchive logs = new LogArchive();

    public LogCommand(string command, string[] args)
    {
        this.command = command;
        this.args = args;
    }

    public override Commands GetCommandType()
    {
        return Commands.log;
    }

    public override string getHelpMessage()
    {
        return "Log Usage: \n" +
               "    No arg/ list : lists all available logs \n" +
               "    create [title] [content]: adds a new log under current user, with content specified \n" +
               "    delete [id]: deletes log with specified id, only current user's log can be deleted \n" +
               "    read [id]: displays log with specified id \n" +
               "    download [terminal-id]: downloads all log from remote terminal to this terminal \n";
    }
    public override IEnumerator Execute(Game gameContext)
    {
       
        if (args == null || args[0] == "list")
        {      
            yield return gameContext.displayTextSlowlyMethod(logs.Display());
        }
        else
        {
            switch (args[0])
            {
                case "create":
                    if (args.Length != 3)
                    {
                        yield return gameContext.displayTextSlowlyMethod("log create takes 2 arguments \n");
                    }
                    else
                    {
                        logs.Add(new Log(args[2], gameContext.user, gameContext.currentTerminal.name, args[1], Utilities.getFutureTime()));
                    }
                    break;
                case "delete":
                    if (args.Length != 2)
                    {
                        yield return gameContext.displayTextSlowlyMethod("log delete takes 1 argument \n");
                    }
                    else
                    {
                        int id = 0;
                        if (int.TryParse(args[1],out id)){ 

                            if (logs.Remove(id, gameContext.user))
                            {
                                yield return gameContext.displayTextSlowlyMethod("Sucessfuly deleted "+ id + " \n");
                            }
                            else
                                yield return gameContext.displayTextSlowlyMethod("Couldnt delete log, wrong user or non-existant id");
                        }
                        else
                        {
                            yield return gameContext.displayTextSlowlyMethod("id should be an int \n");
                        }
                    }
                    break;
                case "read":
                    if (args.Length != 2)
                    {
                        yield return gameContext.displayTextSlowlyMethod("log read takes 1 argument \n");
                    }
                    else
                    {
                        int id = 0;
                        if (int.TryParse(args[1], out id))
                        {
                            string message = logs.Display(id);
                            if (message != null)
                            {
                                yield return gameContext.displayTextSlowlyMethod(message);
                            }
                            else
                                yield return gameContext.displayTextSlowlyMethod("Unexistant log\n");
                        }
                        else
                        {
                            yield return gameContext.displayTextSlowlyMethod("id should be an int \n");
                        }
                    }
                    break;

                case " download":
                    yield return gameContext.displayTextSlowlyMethod("No download available\n");
                    break;
                default:
                    yield return gameContext.displayTextSlowlyMethod("Unknown argument for log command\n");
                    break;
            }
        }
    }
}
