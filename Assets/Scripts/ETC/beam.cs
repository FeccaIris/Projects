using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class beam : MonoBehaviour
{
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D col)
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

    public void Delete()
    {
        Invoke("Disappear", 0.1f);
    }

    public void Disappear()
    {
        Destroy(gameObject);
    }
}
