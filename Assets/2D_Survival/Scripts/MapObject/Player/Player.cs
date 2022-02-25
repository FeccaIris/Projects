using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{
    public class Player : Unit
    {
        public static Player I;

        public GameObject _unit;
        public GameObject _fireRot;
        public Transform _firePos;
        public Transform _target;

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

            _unit = transform.Find("Unit").gameObject;
            _fireRot = transform.Find("FireRot").gameObject;
            _firePos = transform.Find("FireRot").Find("FirePos").transform;
        }
        void FixedUpdate()
        {
            if (_target != null)
            {
                if (_target.gameObject.activeSelf == false)
                    _target = null;
            }

            ChangeTarget();

            if (_target != null)
            {
                RotatePos();
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
        protected override void Die()
        {
            Destroy(_hpB.gameObject);
            base.Die();
        }

        void UpdateHp()
        {
            _hpB._fill.fillAmount = (float)_hp / _hpMax;
        }

        public void RotatePos()
        {
            Vector3 dir = (_target.position - transform.position).normalized;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            _fireRot.transform.rotation = Quaternion.Lerp(_fireRot.transform.rotation, q, 0.5f);
        }

        public void ChangeTarget()
        {
            List<Enemy> list = new List<Enemy>(GameManager.I._enemies);

            if (list.Count > 0)
            {
                list.Sort(CompareDistance);

                if(list[0] != null)
                    _target = list[0].transform;
                RotatePos();
            }
            else
            {
                _target = null;
            }
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
