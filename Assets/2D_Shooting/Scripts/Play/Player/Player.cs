using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ss
{
    public class Player : MonoBehaviour
    {
        public static Player I;

        Rigidbody2D _rgd;
        Camera _cam;

        #region Child
        GameObject _body;
        SpriteRenderer _body_sp;
        Animator _body_anim;

        GameObject _sword;
        public SpriteRenderer _sword_sp;
        public Animator _sword_anim;
        #endregion

        public Vector3 _offset_idle = new Vector3(1.275f, -0.4f, 0);
        public Vector3 _offset_move = new Vector3(2.334f, -0.901f, 0);

        public bool _flip = false;

        float _maxSpd = 800.0f;

        void Awake()
        {
            I = this;
        }
        public void Init()
        {
            _rgd = GetComponent<Rigidbody2D>();
            _cam = Camera.main;

            _body = transform.Find("Body").gameObject;
            _body_sp = _body.transform.Find("Sprite").GetComponent<SpriteRenderer>();
            _body_anim = _body_sp.GetComponent<Animator>();

            _sword = transform.Find("Sword").gameObject;
            _sword_sp = _sword.transform.Find("Sprite").GetComponent<SpriteRenderer>();
            _sword_anim = _sword_sp.GetComponent<Animator>();

            _sword_sp.transform.localPosition = new Vector3(1.275f, -0.4f, 0);
        }
        void FixedUpdate()
        {
            #region Limit Speed
            if (_rgd.velocity.x > _maxSpd)
            {
                Vector2 vel = _rgd.velocity;
                vel.x = _maxSpd;
                _rgd.velocity = vel;
            }
            if (_rgd.velocity.x < -_maxSpd)
            {
                Vector2 vel = _rgd.velocity;
                vel.x = -_maxSpd;
                _rgd.velocity = vel;
            }
            if (_rgd.velocity.y > _maxSpd)
            {
                Vector2 vel = _rgd.velocity;
                vel.y = _maxSpd;
                _rgd.velocity = vel;
            }
            if (_rgd.velocity.x < -_maxSpd)
            {
                Vector2 vel = _rgd.velocity;
                vel.y = -_maxSpd;
                _rgd.velocity = vel;
            }
            #endregion

            #region Look at mouse
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
            #endregion

            if (Input.GetMouseButton(0))
            {
                _rgd.AddForce(look * 100.0f);
                if(_body_anim.GetBool("Move") != true)
                    _body_anim.SetBool("Move", true);
                if (_sword_anim.GetBool("Move") != true)
                    _sword_anim.SetBool("Move", true);
            }
            else
            {
                _body_anim.SetBool("Move", false);
                _sword_anim.SetBool("Move", false);
            }
            if (Input.GetKey(KeyCode.C))
            {
                _body_anim.SetBool("Move", false);
                _sword_anim.SetBool("Move", false);
                _rgd.velocity *= 0.97f;
            }
        }
    }
}
