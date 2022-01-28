using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Enemy
{
    public GameObject _player;
    Rigidbody2D _rigid;
    SpriteRenderer _sr;

    public float _maxSpeed = 15.0f;

    bool _launched = false;


    protected override void Start()
    {
        _hpMax = 1;
        base.Start();
        _rigid = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        if (FindObjectOfType<Player>() != null)
        {
            _player = Player.I.gameObject;
        }

        if(_player != null)
        {
            StartCoroutine(Trace());
        }
    }

    private void FixedUpdate()
    {
        if(_launched != true)
        {
            LimitSpeed();
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject);
        }
    }

    public void LimitSpeed()
    {
        if (_player != null)    // 속도제한
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
    }


    IEnumerator Trace()
    {
        Vector2 player = _player.transform.position;
        Vector2 pos = transform.position;
        Vector2 move = (player - pos).normalized * _maxSpeed;
        float dis = Vector2.Distance(player, pos);

        while (true)
        {
            if(_player != null)
            {
                player = _player.transform.position;
            }
            pos = transform.position;
            move = (player - pos).normalized * _maxSpeed;
            dis = Vector2.Distance(player, pos);

            if(dis > 30.0f)
            {
                _rigid.AddForce(move);
            }
            else
            {
                Launch();
                break;
            }

            yield return null;
        }
    }

    public void Launch()
    {
        if(_player != null)
        {
            return;
        }

        _launched = false;
        Vector2 player = new Vector2();
        Vector2 move = new Vector2();
        if (_player != null)
            { player = _player.transform.position; }
        Vector2 pos = transform.position;
        if(player != null) 
            { move = (player - pos).normalized * 80.0f; }
        _rigid.AddForce(move);

        Invoke("Del", 3.0f);
    }

    public void Del()
    {
        Destroy(gameObject);
    }
}
