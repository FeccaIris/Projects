using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector_Area : MonoBehaviour
{
    public int _map;

    void Start()
    {
        BoxCollider2D bCol = gameObject.GetComponent<BoxCollider2D>();
        bCol.enabled = false;

        _map = int.Parse(gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Map_Module mm = col.gameObject.GetComponent<Map_Module>();
        if(mm != null)
        {
            int map = int.Parse(col.gameObject.name);
            if(_map == map)
            {
                return;
            }
            Sector._self.Compare(mm, this);

            _map = map;
        }
        
        BoxCollider2D bCol = gameObject.GetComponent<BoxCollider2D>();      // 충돌체크 종료
        if(bCol != null)
        {
            bCol.enabled = false;
        }
    }


}
