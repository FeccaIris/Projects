using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{
    public class PU_LevelUp : PopUp
    {
        public List<Button_Skill> _buttonList;

        public override void Init()
        {
            _buttonList = new List<Button_Skill>(transform.Find("Pad").GetComponentsInChildren<Button_Skill>());

            foreach(Button_Skill b in _buttonList)
            {
                b.Init(this);
            }

            base.Init();
        }

        public void OnSelect()
        {
            if (_cb != null)
                _cb();

            Show(false);
        }
    }
}
