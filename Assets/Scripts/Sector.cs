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
    public void TransPosition(Transform mvTo)                         // �÷��̾ �� ��⿡ �浹�� ��
    {
        transform.position = mvTo.position;
        
        Sector_Center sc = _center.GetComponent<Sector_Center>();     // �߽� ��ü���� �ٸ� ��ü�� Ȱ��ȭ
        sc.TurnOnOthers();
    }
}
