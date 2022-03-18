using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{
    public class Charger : Enemy
    {
        float _distance;
        float _reach = 25.0f;
        Vector3 _targetPos = Vector3.zero;

        public override void Init(float delta = 0)
        {
            _trace = false;
            base.Init(delta);
            _speed = 1.0f;
        }
        protected override void FixedUpdate()
        {
            if (_player != null)
            {
                _distance = Vector3.Distance(transform.position, _player.position);
                if (_distance <= _reach)
                {
                    _trace = true;
                }
            }

            base.FixedUpdate();
        }
        private void OnCollisionEnter2D(Collision2D col)
        {
            if (col.gameObject.tag.Equals("Player"))
            {
                Invoke("EndUse", 0.3f);
            }
        }
    }
}
