using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D _rigid;
    SpriteRenderer _sr;

    public GameObject _player;
    
    public float _maxSpeed = 10.0f;
    public GameObject _dealt;
    public GameObject _explo;
    public bool _alive = true;

    void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        if(FindObjectOfType<Player>() != null)
        {
            _player = Player._inst.gameObject;
        }
        _rigid = GetComponent<Rigidbody2D>();
        StartCoroutine("Faster");
        _dealt.SetActive(false);
        _explo.SetActive(false);

        StartCoroutine("Trace");
    }

    void Update()
    {
        
    }
    private void FixedUpdate()
    {
        if (_player != null)
        {
            if (Vector2.Distance(_player.transform.position, transform.position) > 2.0f)
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
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name.Contains("beam"))
        {
            Destroy(col.gameObject);
        }
    }

    IEnumerator Trace()
    {
        float r = Random.Range(0, 1.0f);
        yield return new WaitForSeconds(r);

        while (true)
        {
            if (_player != null)
            {
                if (_alive == true)
                {
                    Vector2 player = _player.transform.position;
                    Vector2 pos = transform.position;
                    Vector2 move = player - pos;
                    _rigid.AddForce(move);
                }
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator Faster()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            _maxSpeed += 1.0f;
        }
    }

    public void Dealt()
    {
        _alive = false;
        Collider2D c = GetComponent<Collider2D>();
        c.enabled = false;
        _dealt.SetActive(true);
        _sr.enabled = false;
        Invoke("Explosion", 0.5f);
        Invoke("Del", 0.7f);
    }

    public void Explosion()
    {
        _explo.SetActive(true);
    }

    public void Del()
    {
        Destroy(gameObject);
    }
}
