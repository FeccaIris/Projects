using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    Rigidbody2D _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.AddForce(Attack.I._dir * 6000);

        Invoke("Disappear", 1.5f);
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            MapObject mo = col.gameObject.GetComponent<MapObject>();
            if (mo != null)
            {
                mo.Damaged(1);
            }
        }
    }
   
    public void Disappear()
    {
        Destroy(gameObject);
    }
}
