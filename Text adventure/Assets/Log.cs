using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Log 
{
   public string log;
   public string user;
   public string terminal;
   public DateTime date;
    public string title;

    public Log(string log, string user, string terminal, string title, DateTime date)
    {
        this.title = title;
        this.log = log.Replace("\\n",'\n'+"");
        this.user = user;
        this.terminal = terminal;
        this.date = date;
    }



    public string toString()
    {
        return  "["+date + "] " + title;
    }

    public int CompareTo(Log that)
    {
        int comparaison = this.terminal.CompareTo(that.terminal);
        if (comparaison != 0) return comparaison;
        comparaison = this.user.CompareTo(that.user);
        if (comparaison != 0) return comparaison;
        comparaison = this.date.CompareTo(that.date);
        if (comparaison != 0) return comparaison;
        comparaison = this.title.CompareTo(that.title);
        if (comparaison != 0) return comparaison;
        return 0;
    }
}
