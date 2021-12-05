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
        Map map = col.GetComponent<Map>();              // 맵 모듈만 걸러내기
        if (map != null)
        {
            string name = col.gameObject.name;
            _map = int.Parse(name);                     // 할당된 맵 모듈 넘버 저장
        }
        BoxCollider2D bCol = gameObject.GetComponent<BoxCollider2D>();      // 충돌체크 종료
        bCol.enabled = false;
    }
}
