using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class RadarArrow : MonoBehaviour
    {
        public Transform _target;
        public Transform _player;

        void Update()
        {
            _target = Player.I._nearest.transform;
    
        if (_target == null || _target.gameObject.activeSelf == false)
            {
                Destroy(gameObject);
            }
            else
            {
                float dis = Vector3.Distance(_target.position, _player.position);
                if (dis < 15.0f)
                {
                    gameObject.SetActive(false);
                }

                Vector3 dir = _target.position - _player.position;
                dir = dir.normalized;

                float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                Quaternion q = Quaternion.AngleAxis(z - 90, Vector3.forward);       // Vector.forward = zÃà

                transform.rotation = Quaternion.Lerp(transform.rotation, q, 0.5f);
            }
        }
    }
}
