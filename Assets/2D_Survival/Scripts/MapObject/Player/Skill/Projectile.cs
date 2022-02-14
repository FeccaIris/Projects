using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Projectile : Common
    {
        public Vector3 _dir;

        int _pierce = 1;
        int _pierceCount = 0;

        public int Pierce
        {
            get { return _pierce; }
            set { _pierce = value; }
        }
        public int PierceCount
        {
            get { return _pierceCount; }
            set { _pierceCount = value; }
        }

        protected override void Start()
        {
            base.Start();

            _dir = (_target.position - transform.position).normalized;
            float z = Mathf.Atan2(_dir.y, _dir.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(z, Vector3.forward);

            transform.rotation = Quaternion.Lerp(transform.rotation, q, 1.0f);

            _rgd.AddForce(_dir * Speed);

            Invoke("Delete", Maintain);
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag.Equals("Enemy"))
            {
                PierceCount++;
                if (PierceCount >= Pierce)
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
