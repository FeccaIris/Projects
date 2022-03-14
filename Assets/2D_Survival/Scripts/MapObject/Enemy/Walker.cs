using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Walker : Enemy
    {
        public override void Init(float delta = 0)
        {
            base.Init(delta);

            float reverse = 1 / _size;
            reverse = reverse < 0.9f ? 0.9f : reverse;
            _speed *= reverse;
            // ¼Óµµ
            Invoke("EndUse", 20.0f);
        }
    }
}
