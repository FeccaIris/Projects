using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D _rigid;
    public GameObject _player;
    public float _maxSpeed = 10.0f;
    

    void Start()
    {
        _player = Player._inst.gameObject;
        _rigid = GetComponent<Rigidbody2D>();
        StartCoroutine("Faster");
    }

    void Update()
    {
        if(Vector2.Distance(_player.transform.position, transform.position) > 2.0f)
        {
            if (_rigid.velocity.x >= _maxSpeed)
            {
                _rigid.velocity = new Vector2(_maxSpeed, _rigid.velocity.y);
            }
            else if (_rigid.velocity.x <= -_maxSpeed)
            {
                _rigid.velocity = new Vector2(-_maxSpeed, _rigid.velocity.y);
            }
            if (_rigid.velocity.y >= _maxSpeed)
            {
                _rigid.velocity = new Vector2(_rigid.velocity.x, _maxSpeed);
            }
            else if (_rigid.velocity.y <= -_maxSpeed)
            {
                _rigid.velocity = new Vector2(_rigid.velocity.x, -_maxSpeed);
            }
        }
        
        StartCoroutine("Trace");
    }

    IEnumerator Trace()
    {
        Vector2 player = _player.transform.position;
        Vector2 pos = transform.position;
        Vector2 move = player - pos;
        _rigid.AddForce(move);
        yield return new WaitForSeconds(0.5f);
    }

    IEnumerator Faster()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            _maxSpeed += 1.0f;
        }
    }
}
