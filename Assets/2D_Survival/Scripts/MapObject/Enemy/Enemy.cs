using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{
    public class Enemy : Unit, IPoolable
    {
        public Transform _player;

        public float _speed;
        public float _size;
        public float _hpTemp;
        public int _exp;

        public bool _trace = true;

        protected override void Start()
        {
            //_rgd = GetComponent<Rigidbody2D>();
            //_sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
            //Init();
        }
        public virtual void Init(float delta = 0)
        {
            if (_rgd == null)
                _rgd = GetComponent<Rigidbody2D>();
            if (_sprite == null)
                _sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();

            _sprite.color = Color.black;

            _hpTemp = _hpMax;
            _hpTemp += delta;
            // 체력 보정

            _speed = 0.3f;
            _size = 1;
            _exp = 1;

            transform.localScale = Vector3.one; 
            // 초기화

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

            if (this is Walker)
                _hp = _hpTemp;
            else if (this is Charger)
                _hp = 1;

            // 크기 무작위 설정
            //_exp += (int)_hpTemp / 10;

            Invoke("EndUse", 30.0f);
        }
        protected virtual void FixedUpdate()
        {
            if (GameManager.I._isPlaying == true)
            {
                if (_player != null)
                {
                    if (_trace == true)
                        transform.position += (_player.position - transform.position).normalized * _speed;
                }
            }

            if (_player != null)
            {
                float dis = Vector3.Distance(transform.position, _player.position);
                if (dis >= 150.0f)
                    EndUse();
            }
        }
        public override void Damaged(int dmg)
        {
            base.Damaged(dmg);
        }
        protected override void Die()
        {
            GameManager.I._kills++;

            LevelManager.I.GetExp(_exp);

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

        //IPoolable
        public void EndUse()
        {
            if (this is Enemy)
            {
                List<Enemy> list = GameManager.I._enemies;
                if (list != null)
                {
                    foreach (Enemy e in list)
                    {
                        if (e.gameObject == gameObject)
                        {
                            list.Remove(this);
                            break;
                        }
                    }
                }
            }
            gameObject.SetActive(false);
            GameManager.I.RefillPool(gameObject);
        }
    }
}

