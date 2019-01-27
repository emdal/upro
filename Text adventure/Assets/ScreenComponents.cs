using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScreenComponents : Singleton<ScreenComponents>
{
    public Text text;
    public Text sideBar;
    public Text tabs;
    public InputField inputField;
    public bool isReady = false;

    public void initialize(Text text, Text sideBar, Text tabs, InputField inputField)
    {
  
        this.text = text;
        this.sideBar = sideBar;
        this.tabs = tabs;
        this.inputField = inputField;
        isReady = true;
    }

    public IEnumerable waitForAccess()
    {
      yield return new WaitUntil(() => { return isReady; });
    }

}
