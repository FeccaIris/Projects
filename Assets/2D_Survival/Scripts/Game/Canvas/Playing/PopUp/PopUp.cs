using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUp : MonoBehaviour
{
    public virtual void Init()
    {

    }

    public void Show(bool show)
    {
        gameObject.SetActive(show);
    }
}
