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
        Map_Module mm = col.gameObject.GetComponent<Map_Module>();          // 맵 모듈 체크
        if(mm != null)
        {
            if (!( Sector._self._checkModule.Contains(mm) ))
            {
                Sector._self._checkModule.Add(mm);
            }
            if (!( Sector._self._checkSector.Contains(this) ))
            {
                Sector._self._checkSector.Add(this);

                Sector._self.Count();
            }
        }
        


        BoxCollider2D bCol = gameObject.GetComponent<BoxCollider2D>();      // 충돌체크 종료
        if(bCol != null)
        {
            bCol.enabled = false;
        }
    }


}
