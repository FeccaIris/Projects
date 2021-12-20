using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject _player;
    Rigidbody2D _rigid;

    void Start()
    {
        _player = Player._inst.gameObject;
        _rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Vector2 player = _player.transform.position;
        Vector2 pos = transform.position;
        //_rigid.AddForce();
    }
}
