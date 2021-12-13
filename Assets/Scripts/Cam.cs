using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    public GameObject _player;

    void Update()
    {
        gameObject.transform.position = new Vector3(_player.transform.position.x, _player.transform.position.y, gameObject.transform.position.z);
    }
}
