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

    // �̵��� �ٷ� �ݴ� ���� ���� ���� ���� -> �̵� ��ġ�� �÷��̾��� ��ġ��?
    // ��ǥ ���� -> �Ÿ� ��� �ʿ�
}
