using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Common : MonoBehaviour
    {
        public Transform _target;        // 유도 성능 부여시
        public Transform _player;
        public Rigidbody2D _rgd;

        float _ea;
        float _speed = 2500.0f;
        float _cool;
        float _size;
        float _reach;                    // 탐지 범위
        float _maintain = 2.0f;

        public float EA
        {
            get { return _ea; }
            set { _ea = value; }
        }
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        public float Cool
        {
            get { return _cool; }
            set { _cool = value; }
        }
        public float Size
        {
            get { return _size; }
            set { _size = value; }
        }
        public float Reach
        {
            get { return _reach; }
            set { _reach = value; }
        }
        public float Maintain
        {
            get { return _maintain; }
            set { _maintain = value; }
        }


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
