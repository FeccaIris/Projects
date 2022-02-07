using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Move : Control
    {
        public Joystick _joystick;

        bool _moveOn = true;

        Vector3 _direction;
        float _moveSpeed = 0.5f;

        protected override void Start()
        {
            base.Start();
        }

        protected override void FixedUpdate()
        {
            _direction = _joystick.GetDirection();
            _direction *= _moveSpeed;
            if (_moveOn == true)
            {
                

                if (_direction.magnitude != 0.0f)
                {
                    transform.position += _direction;
                }
            }
        }
    }
}
