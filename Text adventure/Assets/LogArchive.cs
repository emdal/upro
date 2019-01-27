using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class LogArchive
{
    private List<Log> logArchive = new List<Log>();

    public void Add(Log log)
    {
        int index = 0;
        for (; index< logArchive.Count; index++)
        {
            if (logArchive[index].CompareTo(log) <= 0)
            {
                break;
            }
        }
        logArchive.Insert(index,log);

    }

    public void Add(Log[] logs)
    {
        foreach (Log log in logs)
        {
            Add(log);
        }
    }
    public bool Remove(int i, string user)
    {
        if (i>=0 && i <= logArchive.Count && logArchive[i].user == user)
        {
            logArchive.RemoveAt(i);
            return true;
        }
        return false;
    }

    public string Display()
    {
        int index = 0;
        string output = "Logs: \n";
        string terminal = null;
        string user = null;
        foreach (Log log in this.logArchive)
        {
            if (terminal != log.terminal)
            {
                output += " Terminal " + log.terminal + ":\n";
                terminal = log.terminal;
            }
            if (user != log.user)
            {
                output += "     User " + log.user + ":\n";
                user = log.user;
            }
            output += "         "+index + "."+ log.toString()+ "\n";
            index++;
        }

        return output;
    }

    public string Display(int index)
    {
        if (index >= 0 && index < logArchive.Count) { 
            Log log = this.logArchive[index];
            return "[" + log.date + "]" + log.title + " by " + log.user + " \n" + log.log + "\n";
        }
        return null;
    }

   
}