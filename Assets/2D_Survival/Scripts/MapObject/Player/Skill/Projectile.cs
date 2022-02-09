using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Projectile : Common
    {
        Transform _firePos;

        protected override void Start()
        {
            base.Start();
            _firePos = _player.Find("FirePos").transform;
        }
    }
}
