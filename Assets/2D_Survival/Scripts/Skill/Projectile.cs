using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Projectile : Skill
    {
        Rigidbody2D _rgd;

        int _pierce;
        int _pierceCount;
        int _dmg;

        public void Init()
        {
            _rgd = GetComponent<Rigidbody2D>();

            _pierce = 0;
            _pierceCount = 0;
            _dmg = 0;

            transform.localScale = new Vector3(1.25f, 0.25f, 1);
        }

        public void Activate(Vector3 dir, float speed, float size, float maintain, int pierce, int dmg)
        {
            _pierce = pierce;
            _dmg = dmg;

            transform.localScale *= size;

            _rgd.AddForce(dir * speed * /*Time.fixedDeltaTime **/ Time.timeScale);
            Invoke("EndUse", maintain);
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag.Equals("Enemy"))
            {
                Enemy e = col.GetComponent<Enemy>();

                _pierceCount++;

                e.Damaged(_dmg);

                if(_pierceCount >= _pierce)
                {
                    EndUse();
                }
            }
        }
    }
}

