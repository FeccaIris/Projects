using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

        public void SetButtons(List<PlayerSkill> list)  // 습득 스킬 버튼 활성화
        {
            if (list.Count > _buttons.Count) return;

            for(int i = 0; i < list.Count; i++)
            {
                _buttons[i]._ps = list[i];

                UpdateButtonLevel();

                _buttons[i].gameObject.SetActive(true);
            }
        }

        public void UpdateButtonLevel()
        {
            foreach(Button_LevelUp b in _buttons)
            {
                Text t = b.transform.Find("Image").Find("Level").GetComponent<Text>();
                t.text = $"Lv.{b._ps._level}";
            }
        }

        public void Test()
        {
            int selectN;
            selectN = Random.Range(0, 3);

            if (SkillManager.I._skList.Count < 1)
                return;
            foreach(PlayerSkill k in SkillManager.I._skList)
            {
                if(k._index == selectN)
                {
                    // 노멀
                }
                else
                {
                    // 리스크/강화 x2
                }
            }
        }
    }
}
