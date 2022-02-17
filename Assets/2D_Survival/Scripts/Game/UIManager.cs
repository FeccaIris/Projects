using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class UIManager : MonoBehaviour
    {
        public static UIManager I;

        public Hpbar _hpB;

        void Awake()
        {
            I = this;
        }

        public void Init()
        {
            _hpB = transform.Find("Playing").Find("Hpbar").GetComponent<Hpbar>();


            _hpB.Init();
        }
    }
}
