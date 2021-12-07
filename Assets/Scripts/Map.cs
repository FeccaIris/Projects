using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public static Map _self;

    void Start()
    {
        _self = this;
    }

    public void MoveMapModule()
    {
        Debug.Log("MMM");
    }
}
