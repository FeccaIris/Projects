using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ss
{
    public class Player : MonoBehaviour
    {
        public static Player I;

        GameObject _unit;
        SpriteRenderer _body;
        SpriteRenderer _sword;

        void Awake()
        {
            I = this;
        }
        public void Init()
        {
            _unit = transform.Find("Unit").gameObject;
            _body = _unit.GetComponent<SpriteRenderer>();
            _sword = _body.transform.Find("Sword").GetComponent<SpriteRenderer>();
        }
        void FixedUpdate()
        {
            Vector3 mPos = Input.mousePosition;
            mPos = Camera.main.ScreenToWorldPoint(mPos);

            Vector3 look = (mPos - transform.position).normalized;

            if(look.x < 0)
            {
                _body.flipY = true;
                
            }
            else
            {
                _body.flipY = false;
                
            }

            float z = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(z, Vector3.forward);
            _unit.transform.rotation = Quaternion.Lerp(_unit.transform.rotation, q, 1.0f);
        }
    }
}
