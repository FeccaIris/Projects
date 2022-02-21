using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Projectile : MonoBehaviour
    {
        Rigidbody2D _rgd;
        GameObject _explosion;

        int _pierce;
        int _pierceCount;
        int _dmg;

        public void Activate(Vector3 dir, float speed, float size, float maintain, int pierce, int dmg)
        {
            _rgd = GetComponent<Rigidbody2D>();
            _explosion = transform.Find("Explosion").gameObject;
            _explosion.SetActive(false);

            _pierce = pierce;
            _dmg = dmg;

            transform.localScale *= size;

            _rgd.AddForce(dir * speed * /*Time.fixedDeltaTime **/ Time.timeScale);
            Invoke("Del", maintain);
        }
        void Del()
        {
            Destroy(gameObject);
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
                    Del();
                }
            }
        }
    }
}

