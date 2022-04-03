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
            if(_rgd == null)
                _rgd = GetComponent<Rigidbody2D>();
            if(_sprite == null)
                _sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
            if(_player == null)
                _player = Player.I;

            _ps = ps;

            transform.localScale *= _ps._size;
            if (_ps._index.Equals(1))
            {
                _sprite.color = new Color(255, 255, 255);
            }
            else if (_ps._index.Equals(2))
            {
                _sprite.color = new Color(255, 255, 255);
            }

            Active();
        }
        void Active()
        {
            if (_ps._isProjectile)
            {
                transform.position = _player.transform.position;
                gameObject.SetActive(true);
                _rgd.AddForce(_ps._targetPos * _ps._speed * Time.fixedDeltaTime * TimeCor);
                _rgd.velocity = Vector2.zero;
            }
            else
            {
                if (_ps._isRandom)
                    transform.position = _player.transform.position;
                else
                    transform.position = _player.transform.position;

                gameObject.SetActive(true);
            }
            Invoke("EndUse", _ps._maintain);
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            Enemy e = col.GetComponent<Enemy>();
            if (e != null)
            {
                if (_ps._isProjectile == true)
                {
                    e.Damaged(_ps._dmg);
                    _pierceCount++;
                    if (_pierceCount >= _ps._pierce)
                        EndUse();
                }
                else
                {
                    if (_ps._doesStay)
                    {
                        e.OnSkill(_ps);
                    }
                    else
                    {
                        e.Damaged(_ps._dmg);
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
