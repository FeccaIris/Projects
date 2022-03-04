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

        public LevelUp _lvUp;

        void Awake()
        {
            I = this;
        }

        public void Init()
        {
            _hpB = transform.Find("Playing").Find("UI").Find("Hpbar").GetComponent<Hpbar>();
            _expB = transform.Find("Playing").Find("UI").Find("Expbar").GetComponent<Expbar>();
            _lvUp = transform.Find("Playing").Find("PopUp").Find("LevelUp").GetComponent<LevelUp>();

            _hpB.Init();
            _expB.Init();
            _lvUp.Init();
        }

        public void LevelUP()
        {
            _lvUp.LevelUP();
        }
    }
}
