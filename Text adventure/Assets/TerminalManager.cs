using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TerminalManager 
{

    public List<Terminal> terminals = new List<Terminal>();
    private int CurrentTerminal = 0;
    Text tabs;
    


    public TerminalManager(Text tabs,Terminal currentTerminal)
    {
        this.tabs = tabs;
        this.terminals.Add(currentTerminal);
        display();

    }

    private void display()
    {
        int displayingTerminal = CurrentTerminal;
        string tabText = "||"+ terminals[CurrentTerminal].name + "||";
        while(true)
        {
            displayingTerminal = (displayingTerminal + 1) % terminals.Count;
            if (displayingTerminal == CurrentTerminal)
                break;
            string nextTab = tabText + terminals[displayingTerminal].name + "|";
            if (Utilities.isPlacableOnLine(tabs, nextTab))
            {
                tabText = nextTab;
            }
            else
                break;
        }
        tabs.text = tabText;
    }
    
    public Terminal getNextTerminal(){
        CurrentTerminal= (CurrentTerminal + 1)%terminals.Count ;
        display();
        return terminals[CurrentTerminal];
    }

    public List<string> getTerminalNames(int range)
    {

        List<string> names = new List<string>();
        for (int i =CurrentTerminal; i< CurrentTerminal + range; i++)
        {
                names.Add(terminals[i].name);
        }
            return names;
        }

    public void addTerminal(Terminal t){
        terminals.Add(t); 
    } 

    public void removeTerminal(string t)
    {

        terminals.Find((Terminal current) => { return current.name == t; });
    }
}
