using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;



public class Game : MonoBehaviour {
    [SerializeField] Text text;
    [SerializeField] Text sideBar;
    [SerializeField] Text tabs;
    [SerializeField] AudioSource keyPressed;
    [SerializeField] InputField inputField;

  


    
    const float DISPLAY_SPEED = 0.01f;
    public string user = "Guest User";

    public Terminal currentTerminal;
    private TerminalManager terminals;
    

    

	// Use this for initialization
	void Start () {
        text.text = "";
        Debug.Log(sideBar);
       
        
        StartCoroutine(boot());

         
    }
     
    
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            currentTerminal = terminals.getNextTerminal();
        }
        currentTerminal.updateTerminal();
        currentTerminal.displayText();
        
    }

    void OnKeyPressed( string hey)
    {

         keyPressed.Play();
    }
    void lauchCommandEntered(string command)
    {
        StartCoroutine(onCommandEnteredAsync(command));
    }
    IEnumerator onCommandEnteredAsync(string input)
    {
        inputField.interactable = false;
        inputField.text = "";
        Command command =Command.createCommand(input);
        
        
        yield return command.Execute(this);
        inputField.interactable = true;

        giveFocusToCmd();

    }

    private void giveFocusToCmd()
    {
        EventSystem.current.SetSelectedGameObject(inputField.gameObject, null);
        inputField.OnPointerClick(new PointerEventData(EventSystem.current));
    }

   

    public IEnumerator displayTextSlowlyMethod(string message, float delayBetweenLetters = DISPLAY_SPEED, bool withSound = true)
    {

        yield return currentTerminal.displayTextSlowlyMethod(message, delayBetweenLetters, withSound);

    }

    IEnumerator boot()
    {
        ScreenComponents.Instance.initialize(text, sideBar, tabs, inputField);
        Entity[] entities = Utilities.GetAllInstances<Entity>();
        // initialize all components that use ScreenComponents
        foreach (Entity e in entities)
        {
            e.initialize();
        }
        


        terminals = new TerminalManager(tabs, currentTerminal);

        inputField.onValueChanged.AddListener(OnKeyPressed);
        inputField.onEndEdit.AddListener(lauchCommandEntered);


        inputField.interactable = false;
        currentTerminal.AddToText("Press any keys to boot system\n");
        yield return currentTerminal.makeTextFlash();

        currentTerminal.clearText();
        currentTerminal.AddToText("       Status:[░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░░]\n");

        yield return displayTextSlowlyMethod("Connecting to main ship systems",DISPLAY_SPEED);
        yield return displayTextSlowlyMethod("...\n", DISPLAY_SPEED*10);
        yield return displayTextSlowlyMethod("Main link failed, trying fall back procedure", DISPLAY_SPEED);
        yield return displayTextSlowlyMethod("...\n", DISPLAY_SPEED*10);
        yield return displayTextSlowlyMethod("Connection Sucessful, launching main command interpreter\n", DISPLAY_SPEED);
        yield return displayTextSlowlyMethod("Terminal Operational, use help to view avaible commands\n", DISPLAY_SPEED);
        yield return displayTextSlowlyMethod("12345678911234567892123456789312345678941234567895123456789612345678971234567898123456789912345678901234567891123456789212345678931234567894123456789\n", DISPLAY_SPEED);
        currentTerminal.AddToText("=========boot complete=========\n");



        inputField.interactable = true;
        giveFocusToCmd();


    }
}
