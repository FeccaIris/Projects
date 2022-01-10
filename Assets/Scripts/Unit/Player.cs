using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player _inst;
    public GameObject _atk;

    public GameObject _dealt;
    public GameObject _explo;

    void Awake()
    {
        _inst = this;
    }

    void Start()
    {
        _dealt.SetActive(false);
        _explo.SetActive(false);
        //StartCoroutine(Cursor());
    }

    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Enemy")
        {
            Dealt();
        }
    }

    IEnumerator Cursor()
    {
        while (true)
        {
            Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouse.z = transform.position.z;
            Vector3 pos = transform.position;
            Vector3 dir = pos - mouse;
            float angle = Mathf.Atan2(dir.y, dir.x) * 180 / Mathf.PI;
            transform.Rotate(0, 0, angle);
            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Dealt()
    {
        _dealt.SetActive(true);
        Invoke("Explosion", 0.5f);
        Invoke("Del", 0.7f);
    }

    public void Explosion()
    {
        _explo.SetActive(true);
    }

    public void Del()
    {
        Destroy(gameObject);
    }
}
