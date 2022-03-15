using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{
    public class Unit : MapObject
    {
        public Rigidbody2D _rgd;
        public SpriteRenderer _sprite;
        
        public float _hp = 0;
        public float _hpMax = 0;


        protected override void Start()
        {
            base.Start();
            _hp = _hpMax;
            _rgd = GetComponent<Rigidbody2D>();
            _sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        }

        public virtual void Damaged(int dmg)
        {
            if (gameObject.activeSelf != false)
                StartCoroutine(Hit());

            _hp -= dmg;
            if(_hp <= 0)
            {
                Die();
            }
        }
        IEnumerator Hit()
        {
            _sprite.color = Color.white;
            yield return new WaitForSeconds(0.1f);
            _sprite.color = Color.black;
        }
        protected virtual void Die()
        {
            Destroy(gameObject);
        }

        
    }
}
