using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector_Center : Sector_Area
{
    public List<Sector_Area> _saList = new List<Sector_Area>();

    protected override void Start()
    {
        base.Start();

        Transform parent = gameObject.transform.parent;             // ������ ��ü �ҷ�����
        int cc = parent.childCount;

        List<GameObject> list = new List<GameObject>();             // ���� ��� ����Ʈ �ۼ�
        for (int i = 0; i < cc; i++)
        {
            list.Add(parent.GetChild(i).gameObject);

            Sector_Area sa = list[i].GetComponent<Sector_Area>();
            _saList.Add(sa);
        }
    }

    public void TurnOnOthers()                                      // ������ ��ü Ȱ��ȭ -> �浹üũ
    {
        foreach (Sector_Area x in _saList)
        {
            BoxCollider2D bCol = x.gameObject.GetComponent<BoxCollider2D>();
            bCol.enabled = true;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        base.OnTriggerEnter2D(col);

        GameObject map = Sector._self._map;
        Transform parent = map.transform.parent;
        int cc = parent.childCount;

        



        // �浹üũ �Ϸ�, �� ������ �� ��� �̵�
    }
}
