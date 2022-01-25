using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : Enemy
{
    public GameObject _player;

    Rigidbody2D _rgd;

    protected override void Start()
    {
        base.Start();
        _rgd = GetComponent<Rigidbody2D>();
    }

    public void Launch()
    {

    }
}
