using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player _inst;

    public Transform _firePos;
    public GameObject _atk;
    public GameObject _beam;
    public GameObject _dealt;
    public GameObject _explo;

    public Vector2 _dir;

    void Awake()
    {
        _inst = this;
    }

    void Start()
    {
        _dealt.SetActive(false);
        _explo.SetActive(false);
        _beam.SetActive(false);
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        _dir = _firePos.position - transform.position;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            Dealt();
        }
    }

    public void Dealt()
    {
        _dealt.SetActive(true);
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

    public void ATKInvoke(float inv = 2.0f)
    {
        _atk.SetActive(true);
        Invoke("ATKFalse", inv);
    }

    public void ATKFalse()
    {
        _atk.SetActive(false);
    }
}
