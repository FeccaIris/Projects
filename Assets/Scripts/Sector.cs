using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour
{
    public static Sector _self;

    private void Start()
    {
        _self = this;
    }
    public void TransPosition(Transform mvTo)
    {
        transform.position = mvTo.position;

        /*
        int cc = gameObject.transform.childCount;
        for(int i = 0; i<cc; i++)
        {
            GameObject go = gameObject.transform.GetChild(i).gameObject;
        }
        */
    }
}
