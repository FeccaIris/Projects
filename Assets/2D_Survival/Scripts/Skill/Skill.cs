using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{
    public class Skill : MonoBehaviour, IPoolable
    {
        public const float TimeCor = 1000.0f;

        public Rigidbody2D _rgd;

        PlayerSkill _ps;
        Player _player;

        int _pierceCount = 0;

        public void EndUse()
        {
            gameObject.SetActive(false);
            GameManager.I.RefillPool(gameObject);
        }

        public virtual void Init(PlayerSkill ps)
        {
            _rgd = GetComponent<Rigidbody2D>();
            _ps = ps;
            _player = Player.I;
        }

        public void Activate()
        {
            transform.position = _ps._startPos;
            gameObject.SetActive(true);

            if (_ps._doesStay == true)
                Invoke("EndUse", _ps._maintain);
        }

        void FixedUpdate()
        {

        }
        void OnTriggerEnter2D(Collider2D col)
        {
            Enemy e = col.GetComponent<Enemy>();
            if (e != null)
            {
                if (_ps._doesMultihit == true)
                {
                    e.OnSkill(_ps);
                }
                else
                {
                    e.Damaged(_ps._dmg);

                    if(_ps._isProjectile == true)
                    {
                        _pierceCount++;
                        if (_pierceCount >= _ps._pierce)
                            EndUse();
                    }
                }
            }
        }
        void OnTriggerExit2D(Collider2D col)
        {
            Enemy e = col.GetComponent<Enemy>();
            if (e != null)
            {
                if (_ps._doesMultihit == true)
                {
                    e.ExitSkill();
                }
            }
        }
    }
}
