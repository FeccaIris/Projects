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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 mouse = Input.mousePosition;
            mouse = Camera.main.ScreenToWorldPoint(mouse);
            GameObject prefab = Resources.Load("Bullet") as GameObject;
            GameObject go = Instantiate(prefab);
        }
    }
}
