using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{
    public class Player : Unit
    {
        public static Player I;

        public GameObject _spriteObj;
        public GameObject _forwardObj;

        public Transform _target;

        public Vector3 _forward;

        public float _distance;
        public bool _immune = false;

        Hpbar _hpB;

        private void Awake()
        {
            I = this;
        }

        public void Init()
        {
            _hpB = UIManager.I._hpB;

            UpdateHp();
        }

        protected override void Start()
        {
            base.Start();

            _spriteObj = transform.Find("Sprite").gameObject;
            _forwardObj = _spriteObj.transform.Find("Forward").gameObject;
        }
        void FixedUpdate()
        {
            _forward = (_forwardObj.transform.position - transform.position).normalized;

            if (_target != null)
            {
                if (_target.gameObject.activeSelf == false)
                    _target = null;
            }

            ChangeTarget();

            if(_target != null)
                _distance = Vector3.Distance(transform.position, _target.position);
        }
        public void ChangeTarget()
        {
            List<Enemy> list = GameManager.I._enemies;

            if (list.Count > 0)
            {
                list.Sort(CompareDistance);

                if (list[0] != null)
                    _target = list[0].transform;
            }
            else
            {
                _target = null;
            }
        }
        private void OnCollisionStay2D(Collision2D col)
        {
            if (col.gameObject.tag.Equals("Enemy"))
            {
                Damaged(1);
                UpdateHp();
            }
        }
        public override void Damaged(int dmg)
        {
            if(_immune == false)
                base.Damaged(dmg);
        }
        protected override void Die()
        {
            Destroy(_hpB.gameObject);
            base.Die();
            GameManager.I.GameOver();
        }
        public void ImmuneFor(float time = 0.5f)
        {
            _sprite.color = Color.white;
            _immune = true;

            Invoke("Breakimmune", time);
        }
        public void Breakimmune()
        {
            _sprite.color = Color.black;
            _immune = false;
        }


        void UpdateHp()
        {
            _hpB._fill.fillAmount = (float)_hp / _hpMax;
        }

        public int CompareDistance(Enemy a, Enemy b)
        {
            if (a != null && b != null)
            {
                float d1 = 0, d2 = 0;

                Vector3 pos1 = a.transform.position;
                Vector3 pos2 = b.transform.position;

                d1 = Vector3.Distance(transform.position, pos1);
                d2 = Vector3.Distance(transform.position, pos2);

                if (d1 < d2)
                {
                    return -1;
                }
                else if (d1 > d2)
                {
                    return 1;
                }
            }

            return 0;
        }
    }
}
