using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Enemy : Unit
    {
        protected Player _player;

        protected override void Start()
        {
            base.Start();

            _player = Player.I;
        }
        protected override void Update()
        {
            base.Update();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            transform.position += new Vector3(0.1f, 0, 0);
        }
    }
}

