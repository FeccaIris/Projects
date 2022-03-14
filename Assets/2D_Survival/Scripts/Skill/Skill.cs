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
        SpriteRenderer _sprite;

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
            _sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();

            transform.position = ps._startPos;

            transform.localScale *= _ps._size;

            if (_ps._index.Equals(1))
            {
                _sprite.color = new Color(255, 255, 255);
            }
            else if (_ps._index.Equals(2))
            {
                _sprite.color = new Color(255, 255, 255);
            }
        }

        public void Projectile()
        {
            gameObject.SetActive(true);
            _rgd.AddForce(_ps._targerPos * _ps._speed * Time.fixedDeltaTime * TimeCor);
            _rgd.velocity = Vector2.zero;

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
