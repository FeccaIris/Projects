using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector_Area : MonoBehaviour
{
    public int _filled;
    private void OnTriggerEnter2D(Collider2D col)
    {
        Map map = col.GetComponent<Map>();      // �Ҵ�Ǿ� �ִٸ� ��ȯ
        if (map != null)
        {
            string name = col.gameObject.name;
            _filled = int.Parse(name);
        }
    }
}
