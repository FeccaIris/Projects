using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{

    public class Button_LevelUp : Button
    {
        public int _index;
        public PlayerSkill _ps;
        public Category _cat;
        public Text _lv;
        public Text _txt;

        public void Init()
        {
            _lv = transform.Find("Level").GetComponent<Text>();
            _txt = transform.Find("Text").GetComponent<Text>();
        }
    }
}
