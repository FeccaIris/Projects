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

        List<GameObject> list = new List<GameObject>();             // 섹터 모듈 리스트 작성
        for (int i = 0; i < cc; i++)
        {
            list.Add(parent.GetChild(i).gameObject);

            Sector_Area sa = list[i].GetComponent<Sector_Area>();
            _saList.Add(sa);
        }
    }

    public void TurnOnOthers()                                      // 나머지 개체 활성화 -> 충돌체크
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

        



        // 충돌체크 완료, 빈 공간에 맵 모듈 이동
    }
}
