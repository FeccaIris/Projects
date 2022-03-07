using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class SecondTab : LevelUpPU
    {
        List<Button_LevelUp> _buttons;

        public override void Init(LevelUp owner)
        {
            Button_LevelUp[] arr = transform.Find("Buttons").GetComponentsInChildren<Button_LevelUp>();
            _buttons = new List<Button_LevelUp>(arr);

            foreach(Button_LevelUp b in _buttons)
            {
                b.onClick.AddListener(delegate ()
                {
                    _owner.OnOff(_owner._thirdTab, this);
                });
            }
            base.Init(owner);
        }

        public void SetButtons(PlayerSkill ps)
        {

        }
    }
}
