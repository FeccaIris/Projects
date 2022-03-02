using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Area : Skill
    {
        Collider2D _col;

        int _dmg;
        float _interval;

        public override void Init()
        {
            base.Init();
            _col = GetComponent<Collider2D>();

            transform.localScale = Vector3.one;
        }

        public void Activate(Skill_Area sa)
        {
            _dmg = sa.Damage;
            _interval = sa.Interval;

            StartCoroutine(Activated());
        }

        IEnumerator Activated()
        {
            while (true)
            {
                yield return new WaitUntil(() => Time.timeScale > 0);
                _col.enabled = true;
                yield return new WaitForSeconds(0.1f);
                _col.enabled = false;
                yield return new WaitForSeconds(_interval);
            }
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.tag.Equals("Enemy"))
            {
                Enemy e = col.GetComponent<Enemy>();

                e.Damaged(_dmg);
            }
        }
    }
}
