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
        public Vector3 _targetDir;

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

            StartCoroutine(Bullet());
        }
        protected override void Update()
        {
            base.Update();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();

            if (_target != null)
            {
                Vector3 dir = (_target.position - transform.position).normalized;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
                _fireRot.transform.rotation = Quaternion.Lerp(_fireRot.transform.rotation, q, 0.5f);
            }
        }

        IEnumerator Bullet()
        {
            GameObject prefab = Resources.Load("SV_Bullet") as GameObject;

            while (true)
            {
                yield return new WaitForSeconds(1.0f);
                
                GameObject go = Instantiate(prefab);
                go.transform.position = _firePos.position;
                go.transform.rotation = _firePos.rotation;
                Bullet b = go.GetComponent<Bullet>();
                b._dir = (_target.position - transform.position).normalized;
                b.Go();
            }
        }
    }
}
