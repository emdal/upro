using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName ="Entity")]
public class Entity : ScriptableObject {

    [HideInInspector]public Entity parent = null;


    [TextArea(10,20)][SerializeField] protected string onExamine;
    protected State<string,int> state;
    protected string type = "Default";
    [SerializeField] protected string initialName ="No name";
    [HideInInspector] public string name;

    public virtual void  initialize()
    {
        name = initialName;
    }

    public string examine()
    {
        return onExamine;
    }


}
