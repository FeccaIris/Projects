using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Unit : MapObject
    {
        protected Rigidbody2D _rgd;
        protected float _hp = 0;
        protected float _hpMax = 0;

        protected override void Start()
        {
            base.Start();
            _hp = _hpMax;
            _rgd = GetComponent<Rigidbody2D>();
        }
        protected override void Update()
        {
            base.Update();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }
}
