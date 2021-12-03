using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector_Center : Sector_Area
{
    public List<Sector_Area> _saList = new List<Sector_Area>();

    protected override void Start()
    {
        base.Start();

        Transform parent = gameObject.transform.parent;             // 나머지 개체 불러오기
        int cc = parent.childCount;

        List<GameObject> list = new List<GameObject>();
        for (int i = 0; i < cc; i++)
        {
            list.Add(parent.GetChild(i).gameObject);

            Sector_Area sa = list[i].GetComponent<Sector_Area>();
            _saList.Add(sa);
        }
    }

    public void TurnOnOthers()          // 나머지 개체 활성화 -> 충돌체크
    {
        foreach (Sector_Area x in _saList)
        {
            BoxCollider2D bCol = x.gameObject.GetComponent<BoxCollider2D>();
            bCol.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Map map = col.gameObject.GetComponent<Map>();
        if(map != null)
        {
            string name = col.gameObject.name;
            _map = int.Parse(name);                     // 할당된 맵 모듈 넘버 저장
        }

        BoxCollider2D bCol = gameObject.GetComponent<BoxCollider2D>();
        bCol.enabled = false;

    }
}
