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
            MapObject mo = col.gameObject.GetComponent<MapObject>();
            if(mo != null)
            {
                mo.Damaged(1);
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
