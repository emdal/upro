
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
[CreateAssetMenu(menuName = "Door")]
public class Door: Entity
{
    [HideInInspector] public List<Room> connectedRooms;
    [SerializeField] protected List<Room> initialConnectedRooms;



    public override void initialize()
    {
        base.initialize();
    }

}