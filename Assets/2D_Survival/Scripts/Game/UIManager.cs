using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class UIManager : MonoBehaviour
    {
        public static UIManager I;

        public Hpbar _hpB;
        public Expbar _expB;
        public List<PopUp> _puList;
        public PU_LevelUp _levelUp;

        void Awake()
        {
            I = this;
        }

        public void Init()
        {
            _hpB = transform.Find("Playing").Find("UI").Find("Hpbar").GetComponent<Hpbar>();
            _expB = transform.Find("Playing").Find("UI").Find("Expbar").GetComponent<Expbar>();

            _puList = new List<PopUp>(transform.Find("Playing").Find("PopUp").GetComponentsInChildren<PopUp>());
            foreach(PopUp pu in _puList)
            {
                pu.Init();

                if(pu is PU_LevelUp)
                {
                    _levelUp = pu as PU_LevelUp;
                }
            }

            _hpB.Init();
            _expB.Init();
        }
    }
}
