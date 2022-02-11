using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Enemy : Unit
    {
        public Transform _player;

        float _speed = 0.3f;
        int _size = 1;

        protected override void Start()
        {
            base.Start();

            if(Player.I != null)
                _player = Player.I.transform;
            _size = Random.Range(1, 4);
            if (_size < 3)
                _size = 1;
            transform.localScale *= _size;
        }
        protected override void Update()
        {
            base.Update();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
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

