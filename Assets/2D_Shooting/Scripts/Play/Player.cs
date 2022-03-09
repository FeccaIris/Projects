using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ss
{
    public class Player : MonoBehaviour
    {
        public static Player I;

        GameObject _body;
        GameObject _sword;

        SpriteRenderer _body_sp;
        SpriteRenderer _sword_sp;

        bool _flip = false;

        void Awake()
        {
            I = this;
        }
        public void Init()
        {
            _body = transform.Find("Body").gameObject;
            _sword = transform.Find("Sword").gameObject;

            _body_sp = _body.transform.Find("Sprite").GetComponent<SpriteRenderer>();
            _sword_sp = _sword.transform.Find("Sprite").GetComponent<SpriteRenderer>();

            _sword_sp.transform.position = new Vector3(1.275f, -0.4f, 0);
        }
        void FixedUpdate()
        {
            Vector3 mPos = Input.mousePosition;
            mPos = Camera.main.ScreenToWorldPoint(mPos);

            Vector3 look = (mPos - transform.position).normalized;

            float z = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg;
            Quaternion q = Quaternion.AngleAxis(z, Vector3.forward);
            _body.transform.rotation = Quaternion.Lerp(_body.transform.rotation, q, 1.0f);
            _sword.transform.rotation = Quaternion.Lerp(_sword.transform.rotation, q, 1.0f);

            if (look.x < 0)
            {
                if(_flip == false)
                {
                    _flip = true;

                    Vector3 pos = _sword_sp.transform.localPosition;
                    pos.y *= -1;
                    _sword_sp.transform.localPosition = pos;

                    _body_sp.flipY = true;
                    _sword_sp.flipY = true;
                }
            }
            else
            {
                if (_flip == true)
                {
                    _flip = false;

                    Vector3 pos = _sword_sp.transform.localPosition;
                    pos.y *= -1;
                    _sword_sp.transform.localPosition = pos;

                    _body_sp.flipY = false;
                    _sword_sp.flipY = false;
                }
            }

        }
    }
}
