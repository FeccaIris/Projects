using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atk : MonoBehaviour
{
    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        
    }

    void OnEnable()
    {
        Invoke("Disappear", 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Enemy")
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
        gameObject.SetActive(false);
    }
}
