using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Bullet : Projectile
    {
        public Vector3 _dir;

        protected override void Start()
        {
            base.Start();
        }
        public void Go()
        {
            _rgd.AddForce(_dir * _speed);
        }
    }
}
