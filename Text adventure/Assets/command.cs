using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Command 
{
   protected string command;
   protected string[] args;

   public enum  Commands
    {
        //usable command types
        help,
        connect,
        open,
        close,
        log,
        time,
        examine,
        //unusable command types 
        invalid,
        empty

    }

    public virtual string getHelpMessage()
    {
        return "Using base Command";
    }
    public virtual IEnumerator Execute(Game gameContex)
    {
        Debug.Log("Using Default command behavior");
        yield return gameContex.displayTextSlowlyMethod("Using Default command behavior");

    }

    public virtual Commands GetCommandType()
    {
        return Commands.invalid;
    }

    static public Command createCommand(string input)
    {
        Command command;
        if (input.Count(x => x == '"') % 2 != 0){
            return new InvalidCommand("", null);
        };
        List<string> tmp = Utilities.ParseText(input, ' ', '"').Cast<string>().ToList();
        string cmd = null;
        tmp = removeEmptyStrings(tmp);
        if (tmp.Count > 0)
        {
            cmd = tmp[0];
        }
        else
        {
            cmd = "empty";
        }
        string[] args = null;
        if (tmp.Count > 1)
        {
            tmp.RemoveAt(0);
            args = tmp.ToArray();


        }
        Commands emcmd = FindCommandType(cmd);
        switch (emcmd)
        {
            case Commands.help:
                command = new HelpCommand(cmd, args);
                break;

            case Commands.log:
                command = new LogCommand(cmd, args);
                break;
            case Commands.time:
                command = new TimeCommand(cmd, args);
                break;

            case Commands.examine:
                command = new ExamineCommand(cmd, args);
                break;

            default:
                Debug.Log("UnimplementedCommand");
                command = new InvalidCommand(cmd, args);
                break;

            case Commands.empty:
                command = new EmptyCommand(cmd, args);
                break;
            case Commands.invalid:
                command = new InvalidCommand(cmd, args);
                break;
        }


        return command;
    }

    public static Commands FindCommandType(string cmd)
    {
        Commands emcmd;
        try
        {
            emcmd = (Commands)System.Enum.Parse(typeof(Commands), cmd, false);
        }
        catch (System.ArgumentException e)
        {
            emcmd = Commands.invalid;
        }

        return emcmd;
    }

    private static List<string> removeEmptyStrings(List<string> tmp)
    {
        //remove empty strings
        List<string> newList = new List<string>();
        foreach (string str in tmp)
        {
            if (str.Length > 0)
            {
                newList.Add(str);
            }
        }

        tmp = newList;
        return tmp;
    }
}