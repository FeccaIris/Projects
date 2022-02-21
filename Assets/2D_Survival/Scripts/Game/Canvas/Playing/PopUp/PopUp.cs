using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class PopUp : MonoBehaviour
    {
        public delegate void CallBack();
        public CallBack _cb;

        public virtual void Init()
        {
            Show(false);
        }

        public void Show(bool show, CallBack cb = null)
        {
            gameObject.SetActive(show);

            _cb = cb;
        }
    }
}
