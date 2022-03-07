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
                    if(b._ps != null)
                        _owner.ReadyForRF(b._ps);

                    _owner.OnOff(_owner._thirdTab, this);
                });

                b.gameObject.SetActive(false);
            }
            base.Init(owner);
        }

        public void SetButtons(List<PlayerSkill> list)
        {
            if (list.Count > _buttons.Count) return;

            for(int i = 0; i < list.Count; i++)
            {
                _buttons[i]._ps = list[i];
                _buttons[i].gameObject.SetActive(true);
            }
        }
    }
}
