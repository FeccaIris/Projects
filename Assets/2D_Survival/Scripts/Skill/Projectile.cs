using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Projectile : Skill
    {
        int _dmg;
        int _pierce;
        int _pierceCount;

        public void Init()
        {
            _pierceCount = 0;

            transform.localScale = Vector3.one;
        }

        public void Activate(Vector3 dir, float speed, float size, float maintain, int pierce, int dmg)
        {
            _pierce = pierce;
            _dmg = dmg;

            transform.localScale *= size;

            _rgd.AddForce(dir * speed * Time.fixedDeltaTime * TimeCor * Time.timeScale);
            _rgd.velocity = Vector2.zero;

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

        public void Explode()
        {

        }
    }
}

