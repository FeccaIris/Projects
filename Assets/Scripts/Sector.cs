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

    // �̵��� �ٷ� �ݴ� ���� ���� ���� ���� -> �̵� ��ġ�� �÷��̾��� ��ġ��?
    // ��ǥ ���� -> �Ÿ� ��� �ʿ�
}
