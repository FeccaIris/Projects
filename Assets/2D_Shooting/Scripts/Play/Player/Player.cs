using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace ss
{
    public class Player : MonoBehaviour
    {
        public static Player I;

        Camera _cam;
        Rigidbody2D _rgd;
        Animator _anim;

        PlayerSprite _pSprite;

        float _maxSpd = 800.0f;

        #region DoubleClick
        int _clickCount = 0;


        #endregion

        #region Child
        public GameObject _sprite;
        public SpriteRenderer _body_sp;
        public SpriteRenderer _sword_sp;
        #endregion

        #region Offset
        public Vector3 _offset_idle = new Vector3(1.275f, -0.4f, 0);
        public Vector3 _offset_move = new Vector3(2.334f, -0.901f, 0);
        public Vector3 _offset_shoot = new Vector3(1.362f, -0.618f, 0);
        public Vector3 _offset_melee = new Vector3(0.17f, 0, 0);
        public Vector3 _offset_melee_body = new Vector3(1.83f, 1.21f, 0);

        public bool _flip = false;
        #endregion

        void Awake()
        {
            I = this;
        }
        public void Init()
        {
            _cam = Camera.main;
            _rgd = GetComponent<Rigidbody2D>();
            _sprite = transform.Find("Sprite").gameObject;
            _anim = _sprite.GetComponent<Animator>();

            _pSprite = _sprite.GetComponent<PlayerSprite>();
            _pSprite.Init(this);

            _body_sp = _sprite.transform.Find("Body").GetComponent<SpriteRenderer>();
            _sword_sp = _sprite.transform.Find("Sword").GetComponent<SpriteRenderer>();

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
            _sprite.transform.rotation = Quaternion.Lerp(_sprite.transform.rotation, q, 1.0f);

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

            if (GameManager.I._isPlaying == false)
                return;

            Vector3 playerPos = transform.position;
            _cam.transform.position = new Vector3(playerPos.x, playerPos.y, _cam.transform.position.z);

            if (Input.GetMouseButton(0))
            {
                _rgd.AddForce(look * 100.0f);
                if (_anim.GetBool("Move") != true)
                    _anim.SetBool("Move", true);
            }
            else
            {
                _anim.SetBool("Move", false);
            }
            if (Input.GetKey(KeyCode.C))
            {
                _anim.SetBool("Move", false);

                _rgd.velocity *= 0.97f;
            }
            if (Input.GetKey(KeyCode.Space))
            {
                _anim.SetBool("Shoot", true);
            }
            else
            {
                _anim.SetBool("Shoot", false);
            }
            if (Input.GetKey(KeyCode.A))
            {
                _anim.SetBool("Melee", true);
            }
            else
            {
                _anim.SetBool("Melee", false);
            }
        }
    }
}
