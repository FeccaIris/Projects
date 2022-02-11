using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Projectile : Common
    {
        public Vector3 _dir;

        public int _pierce = 1;
        public int _pierceCount = 0;


        protected override void Start()
        {
            base.Start();

            _dir = (_target.position - transform.position).normalized;
            float z = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(z, Vector3.forward);

            transform.rotation = Quaternion.Lerp(transform.rotation, q, 1.0f);

            _rgd.AddForce(_dir * _speed);

            Invoke("Delete", _maintain);
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag.Equals("Enemy"))
            {
                _pierceCount++;
                if (_pierceCount >= _pierce)
                {
                    Delete();
                }
            }
        }

        public void Delete()
        {
            Destroy(gameObject);
        }
    }
}
