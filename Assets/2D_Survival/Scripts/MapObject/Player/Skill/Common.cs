using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Common : MonoBehaviour
    {
        protected Transform _target;
        protected Transform _player;

        protected float _ea;
        protected float _speed;
        protected float _cool;
        protected float _size;
        protected float _reach;
        protected float _maintain;

        protected virtual void Start()
        {
            _player = Player.I.transform;
        }
    }
}
