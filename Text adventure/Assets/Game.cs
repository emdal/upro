using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Game : MonoBehaviour {

    const int MAX_LETTER_WIDTH = 58;
    const int N_LINES = 12;

    [SerializeField] Text text;
    [SerializeField] Text sideBar;
    [SerializeField] AudioSource keyPressed;
    [SerializeField] InputField inputField;
    bool isFlashing = true;

    List<string> lines = new List<string>();
    int firstLineShown = 0;
    bool isKeepingUp = true;

	// Use this for initialization
	void Start () {
        StartCoroutine(makeTextFlash());
        text.text = "";

        AddToText("Press Down to launch");

        //displayTextSlowly("ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ceci est une phrase tres interessante ", 0.005f);

        inputField.onValueChanged.AddListener(OnKeyPressed);
        inputField.onEndEdit.AddListener(OnCommandEntered);

    }

    
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            isFlashing = false;
            firstLineShown--;
            isKeepingUp = false;
            updateSideBar();
            if (firstLineShown < 0)
                firstLineShown = 0;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            firstLineShown++;
            if (firstLineShown >= lines.Count - N_LINES)
            {
                isKeepingUp = true;
                sideBar.text = "<";
            }
            else
            {
                updateSideBar();
            }
        }

        displayText();

    }

    private void updateSideBar()
    {
        sideBar.text = "";
        int position = (lines.Count - (firstLineShown + N_LINES));
        for (int i = 0; i < position; i++)
            sideBar.text += " ";


        sideBar.text += "|";
    }

    void AddToText(string message, bool isNewLine = true)
    {
        int lineLenght = 0;
        int index = 0;
        if (lines.Count > 0 && !isNewLine)
        {
            message = lines[lines.Count - 1] + message;
            lines.RemoveAt(lines.Count - 1);
        }
            
        foreach ( char  letter in message)
        {
            lineLenght++;
            index++;
            if (letter == '\n')
            {
                lineLenght = 0;
            }
            if (lineLenght >= MAX_LETTER_WIDTH)
            {
                message = message.Insert(index, "\n");
                lineLenght = 0;
            }

        }

        List<string> linesToAdd = new List<string>(message.Split('\n'));


      
        lines.AddRange(new List<string>(linesToAdd));



    }

    void displayText()
    {
        text.text= "";

        if (isKeepingUp)
        {
            firstLineShown = lines.Count - N_LINES;
            if (firstLineShown < 0)
                firstLineShown = 0;

        }
        
        for (int i=firstLineShown;i<=firstLineShown + N_LINES && i < lines.Count; i++) {
            text.text += lines[i] + '\n';
        }
    }

    void clearText()
    {
        lines = new List<string>();
    }

    void displayTextSlowly(string message, float delayBetweenLetters = 0.25f)
    {
        inputField.interactable = false;
        StartCoroutine(displayTextSlowlyMethod(message, delayBetweenLetters));
        
    }

    IEnumerator displayTextSlowlyMethod( string message, float delayBetweenLetters=0.25f, bool withSound = true)
    {
        
        foreach ( char letter in message)
        {
            
            AddToText(""+letter, false);
            yield return new WaitForSeconds(delayBetweenLetters);

        }
        inputField.interactable = true;
    }

    void OnKeyPressed( string hey)
    {

         keyPressed.Play();
        Debug.Log("hello");
    }

    void OnCommandEntered(string command)
    {
        if (command == "help")
        {
            displayTextSlowly("Commands: \n log connect open close \n");
        }
    }

    IEnumerator makeTextFlash()
    {
        float incrementValue = 0.1f;
        float increment = incrementValue;

        while (isFlashing)
        {

            Color textColor = text.color;
            
            if (text.color.a <=0 )
                increment = incrementValue;
            
            if (text.color.a >=1)
                increment = -incrementValue;

            textColor.a+= increment;

            text.color = textColor;
            Debug.Log(text.color);
            yield return new WaitForSeconds(0.1f);
        }

        Color textColorFinal = text.color;
        textColorFinal.a = 1;
        text.color = textColorFinal;

    }
}
