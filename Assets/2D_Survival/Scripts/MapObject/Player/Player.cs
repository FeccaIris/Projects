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

        private void Awake()
        {
            I = this;
        }
        protected override void Start()
        {
            base.Start();

            _unit = transform.Find("Unit").gameObject;
            _fireRot = transform.Find("FireRot").gameObject;
            _firePos = transform.Find("FireRot").Find("FirePos").transform;

            //StartCoroutine(Bullet());
        }
        protected override void Update()
        {
            base.Update();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            // 타겟 지정 = 리스트 중 가장 가까운 적 = 거리비교
            ChangeTarget();

            if (_target != null)
            {
                Vector3 dir = (_target.position - transform.position).normalized;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                _fireRot.transform.rotation = Quaternion.Lerp(_fireRot.transform.rotation, q, 0.5f);
            }
        }
        private void OnCollisionStay2D(Collision2D col)
        {
            if (col.gameObject.tag.Equals("Enemy"))
            {
                Damaged(1);
            }
        }


        public void ChangeTarget()
        {
            List<Enemy> list = new List<Enemy>(GameManager.I._enemies);

            if (list.Count > 0)
            {
                list.Sort(CompareDistance);

                if(list[0] != null)
                    _target = list[0].transform;
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

        IEnumerator Bullet()
        {
            GameObject prefab = Resources.Load("SV_Bullet") as GameObject;

            while (true)
            {
                yield return new WaitForSeconds(1.0f);

                if (_target != null)
                {
                    GameObject go = Instantiate(prefab);
                    go.transform.position = _firePos.position;
                    //go.transform.rotation = _firePos.rotation;
                    Projectile p = go.GetComponent<Projectile>();
                    p._target = _target;
                }
            }
        }
    }
}
