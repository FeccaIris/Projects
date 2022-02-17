using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Enemy : Unit
    {
        public Transform _player;

        float _speed = 0.3f;
        float _size = 1;

        protected override void Start()
        {
            base.Start();

            if(Player.I != null)
                _player = Player.I.transform;

            _size = Random.Range(0.5f, 2.0f);
            transform.localScale *= _size;

            float reverse = 1 / _size;
            reverse = reverse < 0.7f ? 0.7f : reverse;
            _speed *= reverse;
        }
        void FixedUpdate()
        {
            if(_player != null)
                transform.position += (_player.position - transform.position).normalized * _speed;
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag.Equals("Attack"))
            {
                Damaged(1);
            }
        }

        protected override void Die()
        {
            base.Die();
            List<Enemy> list = GameManager.I._enemies;
            if (list != null)
            {
                foreach (Enemy e in list)
                {
                    if (e == this)
                    {
                        list.Remove(this);
                        break;
                    }
                }
            }
        }
    }
}

