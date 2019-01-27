
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "AbsItem")]
public class AbstractItem: Entity
{
    [SerializeField] protected int initialWeight = 0;
    [HideInInspector] public int weight = 0;
    [SerializeField] protected bool initialIsPickable;
    [HideInInspector]  public bool isPickable = true;
    
    


    public override void initialize()
    {
        base.initialize();
        this.isPickable = initialIsPickable;
        this.weight = initialWeight;
    }

}