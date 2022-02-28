using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Area : Skill
    {
        int _dmg;
        float _interval;

        public override void Init()
        {
            base.Init();

            transform.localScale = Vector3.one;
        }
    }
}
