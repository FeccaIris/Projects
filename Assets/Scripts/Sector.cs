using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour
{
    public static Sector _self;

    private void Start()
    {
        _self = this;
    }
    public void TransPosition(Transform mvTo)
    {
        transform.position = mvTo.position;

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision);
    }

    // 이동시 바로 반대 방향 섹터 접촉 오류 -> 이동 위치를 플레이어의 위치로?
    // 좌표 오류 -> 거리 계산 필요
}
