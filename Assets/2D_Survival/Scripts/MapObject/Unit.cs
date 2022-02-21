using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Unit : MapObject
    {
        public Rigidbody2D _rgd;
        public int _hp = 0;
        public int _hpMax = 0;

        protected override void Start()
        {
            base.Start();
            _hp = _hpMax;
            _rgd = GetComponent<Rigidbody2D>();
        }

        public virtual void Damaged(int dmg)
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
