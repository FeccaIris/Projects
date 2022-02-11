using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Common : MonoBehaviour
    {
        public Transform _target;       // ���� ���� �ο���
        public Transform _player;
        public Rigidbody2D _rgd;

        public float _ea;
        public float _speed = 2500.0f;
        public float _cool;
        public float _size;
        public float _reach;
        public float _maintain = 2.0f;

        protected virtual void Start()
        {
            _player = Player.I.transform;
            _rgd = GetComponent<Rigidbody2D>();
        }
        protected virtual void FixedUpdate()
        {

        }
    }
}
