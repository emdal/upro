
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Terminal")]
public class Terminal: AbstractItem
{






    public bool isFlashing = false;
    public List<int> lockedLines = new List<int>();
    public List<string> lines = new List<string>();
    public int firstLineShown = 0;
    public bool isKeepingUp = true;
    public bool lastLineIsFinal = false;
    public const float DISPLAY_SPEED = 0.01f;
    int nLines;
    Text textReference;
    Text sideBar;
    
    State<string, int> terminalState = new State<string, int>();


    public Room getLocation()
    {
        return (Room)this.parent;
    }
    public override void initialize()
    {
        base.initialize();


        isFlashing = false;
        lockedLines = new List<int>();
        lines = new List<string>();
        firstLineShown = 0;
        isKeepingUp = true;
        lastLineIsFinal = false;
        State<string, int> terminalState = new State<string, int>();


        this.textReference = ScreenComponents.Instance.text;
        this.sideBar = ScreenComponents.Instance.sideBar;
        this.nLines = Utilities.getNLineFitVertical(textReference);


    }
    public void AddToText(string message, bool isNewLine = true)
    {

        if (lines.Count > 0 && !isNewLine && !lastLineIsFinal)
        {
            message = lines[lines.Count - 1] + message;
            lines.RemoveAt(lines.Count - 1);
        }
        string lineContent = "";
        foreach (char letter in message)
        {
            if (letter == '\n')
            {

                lines.Add(lineContent);
                lineContent = "";
                lastLineIsFinal = true;
                continue;
            }

            if (!Utilities.isPlacableOnLine(this.textReference, lineContent + letter))
            {
                lines.Add(lineContent);
                lineContent = "";

            }
            lastLineIsFinal = false;
            lineContent += letter;
        }
        if (lineContent != "")
        {
            lines.Add(lineContent);
            lastLineIsFinal = false;
        }



    }

    public void clearText()
    {
        lines = new List<string>();
    }

    private string getSideBarValue()
    {

        string sideBarText = "";
        double nLinesAwayFromBottom = (lines.Count - (firstLineShown + nLines));
        nLinesAwayFromBottom = nLinesAwayFromBottom < 0 ? 0 : nLinesAwayFromBottom;
        double maxLineAwayFromBottom = (lines.Count + 0.0 - nLines);
        if (maxLineAwayFromBottom == 0) maxLineAwayFromBottom = 1;
        double position = nLinesAwayFromBottom / maxLineAwayFromBottom;
        double max = 39;
        for (int i = 0; i < max * position; i++)
            sideBarText += " ";


        return (sideBarText += "|");
    }

    public void updateTerminal()
    {
        string sideBarText = sideBar.text;
        if (isFlashing && Input.anyKeyDown)
        {
            isFlashing = false;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {

                firstLineShown--;
                isKeepingUp = false;
                sideBarText = getSideBarValue();
                if (firstLineShown < 0)
                    firstLineShown = 0;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                firstLineShown++;
                if (firstLineShown >= lines.Count - nLines)
                {
                    isKeepingUp = true;
                    sideBarText = "<";
                }
                else
                {
                    sideBarText = getSideBarValue();
                }
            }
        }
        sideBar.text = sideBarText;
    }
    public IEnumerator displayTextSlowlyMethod(string message, float delayBetweenLetters = DISPLAY_SPEED, bool withSound = true)
    {

        foreach (char letter in message)
        {

            AddToText("" + letter, false);
            yield return new WaitForSeconds(delayBetweenLetters);

        }

    }

    public IEnumerator makeTextFlash()
    {
        isFlashing = true;
        float incrementValue = 0.1f;
        float increment = incrementValue;

        while (isFlashing)
        {

            Color textColor = textReference.color;

            if (textReference.color.a <= 0)
                increment = incrementValue;

            if (textReference.color.a >= 1)
                increment = -incrementValue;

            textColor.a += increment;

            textReference.color = textColor;
            yield return new WaitForSeconds(0.1f);
        }

        Color textColorFinal = textReference.color;
        textColorFinal.a = 1;
        textReference.color = textColorFinal;

    }
    public void displayText()
    {
        List<string> oldLines = new List<string>(textReference.text.Split('\n'));
        textReference.text = "";

        int nFree = nLines - lockedLines.Count;


        if (isKeepingUp)
        {
            firstLineShown = lines.Count - nFree;
            if (firstLineShown < 0)
                firstLineShown = 0;

        }
        int screenLine = firstLineShown;
        for (int currentLine = 0; currentLine < nLines; currentLine++)
        {
            if (!lockedLines.Contains(currentLine))
            {
                textReference.text += screenLine >= lines.Count ? "\n" : lines[screenLine] + "\n";
                screenLine++;
            }
            else
            {
                textReference.text += currentLine >= oldLines.Count ? "\n" : oldLines[currentLine] + "\n";
            }

        }

    }

}