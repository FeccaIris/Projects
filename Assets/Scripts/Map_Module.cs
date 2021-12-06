using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Module : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name.Equals("Player"))
        {
            Transform trans = transform;
            Sector._self.TransPosition(trans);      // 이동
        }
    }

    public void MoveMapModule()
    {
        // 비할당 맵모듈 할당 이동
    }
}
