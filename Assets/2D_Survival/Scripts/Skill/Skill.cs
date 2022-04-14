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
        public Collider2D _col;

        int _pierceCount = 0;

        public void EndUse()
        {
            gameObject.SetActive(false);
            GameManager.I.RefillPool(gameObject);
        }

        public virtual void Init(PlayerSkill ps)
        {
            if (_rgd == null)
                _rgd = GetComponent<Rigidbody2D>();
            if (_sprite == null)
                _sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
            if (_player == null)
                _player = Player.I;
            if (_col == null)
                _col = GetComponent<Collider2D>();

            _ps = ps;

            switch (_ps._index)
            {
                case 0:
                    {
                        _sprite.color = new Color(1, 0, 0, 0.5f);
                        break;
                    }
                case 1:
                    {
                        _sprite.color = new Color(0, 1, 0, 0.5f);
                        break;
                    }
                case 2:
                    {
                        _sprite.color = new Color(0, 0, 1, 0.5f);
                        break;
                    }
                default:
                    break;
            }
            transform.localScale = Vector3.one;
            transform.localScale *= _ps._size;

            Active();
        }
        void Active()
        {
            if (_ps._isProjectile)
            {
                transform.position = _player.transform.position;
                gameObject.SetActive(true);
                _rgd.velocity = _ps._targetPos * _ps._speed;

            }
            else
            {
                transform.position = _ps._targetPos;
                gameObject.SetActive(true);
                _col.enabled = true;
                StartCoroutine(TurnCollider());
            }
            Invoke("EndUse", _ps._maintain);
        }

        IEnumerator TurnCollider()
        {
            while (true)
            {
                yield return new WaitForSeconds(_ps._interval);
                _col.enabled = false;
                yield return new WaitForSeconds(_ps._interval);
                _col.enabled = true;
            }
        }

        void FixedUpdate()
        {
            if (!_ps._isProjectile)
            {
                if (_ps._isStatic == true)
                    transform.position = _player.transform.position;
            }
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
                    e.Damaged(_ps._dmg);
                }
            }
        }
    }
}
