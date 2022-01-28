using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector_Area : MonoBehaviour
{
    void Start()
    {
        BoxCollider2D bCol = gameObject.GetComponent<BoxCollider2D>();
        bCol.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Map_Module mm = col.gameObject.GetComponent<Map_Module>();          // �� ��� üũ
        if(mm != null)
        {
            if ( !(Sector._self._checkModule.Contains(mm)) )                // �浹 ����Ʈ �ۼ�
            {
                Sector._self._checkModule.Add(mm);
            }
            if ( !(Sector._self._checkSector.Contains(this)) )
            {
                Sector._self._checkSector.Add(this);
            }
        }
        
        BoxCollider2D bCol = gameObject.GetComponent<BoxCollider2D>();      // �浹üũ ����
        if(bCol != null)
        {
            bCol.enabled = false;
        }
    }


}
