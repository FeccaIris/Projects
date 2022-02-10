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
            _target = Player.I._target;
    
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

                Vector3 dir = _target.position - _player.position;                  // 방향 벡터
                dir = dir.normalized;

                float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;                // 회전 각도
                Quaternion q = Quaternion.AngleAxis(z - 90, Vector3.forward);       // Vector.forward = z축 -> z축 기준으로 회전

                transform.rotation = Quaternion.Lerp(transform.rotation, q, 0.5f);
            }
        }
    }
}
