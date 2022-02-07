using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarArrow : MonoBehaviour
{
    public Transform _enemy;
    public Transform _player;

    void Update()
    {
        Vector3 dir = _enemy.position - _player.position;
        dir = dir.normalized;

        float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(z - 90, Vector3.forward);

        transform.rotation = Quaternion.Lerp(transform.rotation, q, 0.5f);
    }
}
