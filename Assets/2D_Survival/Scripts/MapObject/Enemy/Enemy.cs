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
        [SerializeField] int _hpTemp;

        public void Init(int delta = 0)
        {
            _hpTemp = _hpMax;
            _hpTemp += delta;

            _speed = 0.3f;
            _size = 1;
            _exp = 1;

            transform.localScale = Vector3.one;

            if (Player.I != null)
                _player = Player.I.transform;

            _size = Random.Range(0.5f, 2.0f);
            transform.localScale *= _size;

            if (_size > 1.5f)
                _hpTemp += 2;
            else if (_size > 1.2f)
                _hpTemp += 1;
            else if (_size < 0.7f)
                _hpTemp -= 2;
            else if (_size < 1.0f)
                _hpTemp -= 1;

            _hp = _hpTemp;

            _exp += _hpTemp / 5;

            float reverse = 1 / _size;
            reverse = reverse < 0.9f ? 0.9f : reverse;
            _speed *= reverse;
        }
        protected override void Start()
        {
            _rgd = GetComponent<Rigidbody2D>();
            Init();
        }
        void FixedUpdate()
        {
            if (_player != null)
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

        public void OnSkill(PlayerSkill ps)
        {
            StartCoroutine(OnArea(ps));
        }

        IEnumerator OnArea(PlayerSkill ps)
        {
            while (true)
            {
                Damaged(ps._dmg);
                yield return new WaitForSeconds(ps._interval);
            }
        }

        public void ExitSkill()
        {
            StopAllCoroutines();
        }

        public void EndUse()
        {
            gameObject.SetActive(false);
            GameManager.I.RefillPool(gameObject);
        }
    }
}

