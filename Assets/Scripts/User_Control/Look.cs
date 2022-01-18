using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    void FixedUpdate()
    {
        #region FollowMouseDir
        Vector2 m = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 pos = transform.position;
        Vector2 d = m - pos;
        float z = Mathf.Atan2(d.y, d.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, z);
        #endregion
    }
}
