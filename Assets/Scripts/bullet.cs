using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        //_rb.AddForce( * 1000);

        Invoke("Disappear", 2.0f);
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            Enemy e = col.GetComponent<Enemy>();
            if (e != null)
            {
                e.Dealt();
            }
        }
    }
   
    public void Disappear()
    {
        Destroy(gameObject);
    }
}
