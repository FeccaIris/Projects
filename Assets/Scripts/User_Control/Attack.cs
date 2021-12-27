using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Player._inst._atk.SetActive(true);
        }
    }
}
