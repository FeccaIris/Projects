using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class UIManager : MonoBehaviour
    {
        public static UIManager I;

        public Hpbar _hpB;

        List<PopUp> _puList;

        void Awake()
        {
            I = this;
        }

        public void Init()
        {
            _hpB = transform.Find("Playing").Find("UI").Find("Hpbar").GetComponent<Hpbar>();
            _puList = new List<PopUp>(transform.Find("Playing").Find("PopUp").GetComponentsInChildren<PopUp>());
            foreach(PopUp pu in _puList)
            {
                pu.Show(false);
            }

            _hpB.Init();
        }
    }
}
