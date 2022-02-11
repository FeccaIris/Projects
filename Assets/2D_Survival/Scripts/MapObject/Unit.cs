using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Unit : MapObject
    {
        public Rigidbody2D _rgd;
        public float _hp = 0;
        public float _hpMax = 0;

        protected override void Start()
        {
            base.Start();
            _hp = _hpMax;
            _rgd = GetComponent<Rigidbody2D>();
        }
        protected override void Update()
        {
            base.Update();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }

        protected virtual void Damaged(int dmg)
        {
            _hp -= dmg;
            if(_hp <= 0)
            {
                Die();
            }
        }
        protected virtual void Die()
        {
            Destroy(gameObject);
        }
    }
}
