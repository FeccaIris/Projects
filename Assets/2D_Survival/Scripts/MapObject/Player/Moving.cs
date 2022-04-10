using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace SV
{

    public class Moving : MonoBehaviour
    {
        public Joystick _joystick;

        Rigidbody2D _rgd;
        Player _player;

        Vector3 _direction;
        Vector3 _look;
        float _moveSpeed = 0.5f;

        void Start()
        {
            _rgd = GetComponent<Rigidbody2D>();
            _player = Player.I;
        }

        void FixedUpdate()
        {
            _rgd.velocity = Vector2.zero;

            float h = CrossPlatformInputManager.GetAxis("Horizontal");
            float v = CrossPlatformInputManager.GetAxis("Vertical");

            if (h != 0 || v != 0)
            {
                if (GameManager.I._isPlaying == true)
                {
                    //Move(h, v);
                    _direction = new Vector3(h, v, 0).normalized;
                }
            }
            else
            {
                _direction = _joystick.GetDirection();      // Á¶ÀÌ½ºÆ½ ¹æÇâ ¹Þ¾Æ¿È
                //_look = _direction;
                //_direction *= _moveSpeed * Time.timeScale;
            }

            if (GameManager.I._isPlaying == true)
            {
                _look = _direction;
                _direction *= _moveSpeed * Time.timeScale;

                if (_look != Vector3.zero)
                {
                    _look = (_look.magnitude > 1.0f) ? _look.normalized : _look;
                    float z = Mathf.Atan2(_look.y, _look.x) * Mathf.Rad2Deg;
                    Quaternion q = Quaternion.AngleAxis(z - 90, Vector3.forward);
                    _player._spriteObj.transform.rotation = Quaternion.Lerp(_player._spriteObj.transform.rotation, q, 0.5f);
                }

                if (_direction.magnitude != 0.0f)
                {
                    transform.position += _direction;
                }
            }
        }
        
        void Move(float h, float v)
        {
            Vector3 dir = new Vector3(h, v, 0).normalized;
            Vector3 look = dir;
            dir *= _moveSpeed * Time.timeScale;
            transform.position += dir;

            if (look != Vector3.zero)
            {
                look = (look.magnitude > 1.0f) ? look.normalized : look;
                float z = Mathf.Atan2(look.y, look.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(z - 90, Vector3.forward);
                _player._spriteObj.transform.rotation = Quaternion.Lerp(_player._spriteObj.transform.rotation, q, 0.5f);
            }
        }
    }
}
