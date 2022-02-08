using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Enemy : MonoBehaviour
    {
        protected Player _player;
        protected Rigidbody2D _rgd;


        protected virtual void Start()
        {
            _player = Player.I;
            _rgd = GetComponent<Rigidbody2D>();
        }
        protected virtual void Update()
        {

        }
        protected virtual void FixedUpdate()
        {

        }
    }
}

