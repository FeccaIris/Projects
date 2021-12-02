using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    private void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name.Equals("Player"))
        {
            Transform trans = transform;
            Sector._self.TransPosition(trans);

            int num = int.Parse(gameObject.name);
            Debug.Log(num);
        }
    }
}
