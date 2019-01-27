
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Room")]
public class Room: Entity
{
    //Doors
    [HideInInspector] public List<Door> doors;
    [SerializeField] protected List<Door> initialDoors = null;
    [SerializeField] protected List<Entity> initialContainedItems;
    [HideInInspector] public List<Entity> containedItems;

    public void addItem(Entity e)
    {
        e.parent = this;
        containedItems.Add(e);
    }
    
    
    


    public override void initialize()
    {
        base.initialize();
        this.doors = initialDoors;
        this.containedItems = initialContainedItems;
        foreach (Entity e in containedItems)
        {
            Debug.Log("Items contained:");
            Debug.Log(e.examine());
            e.parent = this;
            Debug.Log(e.GetInstanceID());
        }
        foreach (Door e in doors)
        {
            e.parent = null;
            e.connectedRooms.Add(this);
        }
    }

}