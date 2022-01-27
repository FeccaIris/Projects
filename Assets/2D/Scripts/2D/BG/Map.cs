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

    public void MoveMapModule(List<Sector_Area> toAssi, List<Map_Module> toMove)
    {
        int cc = toMove.Count;
        for(int i = 0; i < cc; i++)
        {
            if (toAssi[i] != null && toMove[i] != null)
            {
                Sector_Area sa = toAssi[i];
                Map_Module mm = toMove[i];

                mm?.Move(sa);
            }
        }
    }
}
