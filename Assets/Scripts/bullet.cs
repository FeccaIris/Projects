using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(Attack.I._dir * 4500);

        Invoke("Disappear", 1.5f);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            Enemy e = col.gameObject.GetComponent<Enemy>();
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
