using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector_Area : MonoBehaviour
{
    public int? _map = null;

    protected virtual void Start()
    {
        BoxCollider2D bCol = gameObject.GetComponent<BoxCollider2D>();
        bCol.enabled = false;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        Map map = col.GetComponent<Map>();              // �� ��⸸ �ɷ�����
        if (map != null)
        {
            string name = col.gameObject.name;
            _map = int.Parse(name);                     // �Ҵ�� �� ��� �ѹ� ����
        }
        BoxCollider2D bCol = gameObject.GetComponent<BoxCollider2D>();      // �浹üũ ����
        bCol.enabled = false;
    }
}
