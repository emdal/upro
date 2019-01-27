using UnityEngine;
using System.Collections;

public class CoRountineHolder : MonoBehaviour
{
    public static CoRountineHolder instance;
    // Use this for initialization
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
