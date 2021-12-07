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
        Map_Module mm = col.gameObject.GetComponent<Map_Module>();          // �� ��� üũ
        if(mm != null)
        {
            Sector._self.Select(this, mm);
        }
        
        BoxCollider2D bCol = gameObject.GetComponent<BoxCollider2D>();      // �浹üũ ����
        if(bCol != null)
        {
            bCol.enabled = false;
        }
    }


}
