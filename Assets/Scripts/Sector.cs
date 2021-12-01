using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : Map
{
    public GameObject _sector;

    void Start()
    {
        _sector = gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            Debug.Log(_sector);
        }
    }

    // 이동시 바로 반대 방향 섹터 접촉 오류 -> 이동 위치를 플레이어의 위치로?
    // 좌표 오류 -> 거리 계산 필요
}
