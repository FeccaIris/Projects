using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Enemy : Unit, IPoolable
    {
        public Transform _player;

        float _speed;
        float _size;
        int _exp;

        Vector3 _default = new Vector3(1, 1, 0);

        public void Init(int delta = 0)
        {
            _hpMax += delta;
            _hp = _hpMax;
            _speed = 0.3f;
            _size = 1;
            _exp = 1;

            transform.localScale = _default;

            if (Player.I != null)
                _player = Player.I.transform;

            _size = Random.Range(0.5f, 2.0f);
            transform.localScale *= _size;

            float reverse = 1 / _size;
            reverse = reverse < 0.9f ? 0.9f : reverse;
            _speed *= reverse;
        }
        protected override void Start()
        {
            base.Start();
            Init();
        }
        void FixedUpdate()
        {
            if(_player != null)
                transform.position += (_player.position - transform.position).normalized * _speed;
        }

        protected override void Die()
        {
            LevelManager.I.GetExp(_exp);

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

            EndUse();
        }

        public void EndUse()
        {

            gameObject.SetActive(false);
            GameManager.I.RefillPool(gameObject);
        }
    }
}

