using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sector : MonoBehaviour
{
    public GameObject _map;
    public GameObject _center;
    public static Sector _self;

    private void Start()
    {
        _self = this;
    }
    public void TransPosition(Transform mvTo)                         // 플레이어가 맵 모듈에 충돌할 때
    {
        transform.position = mvTo.position;
        
        Sector_Center sc = _center.GetComponent<Sector_Center>();     // 중심 개체에서 다른 개체들 활성화
        sc.TurnOnOthers();
    }
}
