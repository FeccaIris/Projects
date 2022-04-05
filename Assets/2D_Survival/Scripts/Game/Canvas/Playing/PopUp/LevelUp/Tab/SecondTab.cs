using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{
    public enum Category
    {
        DAMAGE,
        COOL,
        EA,
        MAINTAIN,
        REACH,
        SPEED,
        PIERCE,
        INTERVAL,
    }

    public class SecondTab : LevelUpPU
    {
        public List<Button_LevelUp> _buttons;

        public override void Init(LevelUp owner)
        {
            Button_LevelUp[] arr = transform.Find("Buttons").GetComponentsInChildren<Button_LevelUp>();
            _buttons = new List<Button_LevelUp>(arr);

            for(int i = 0; i < _buttons.Count; i++)
            {
                Button_LevelUp b = _buttons[i];
                b._index = i;
                Debug.Log($"{b}{b._index}");
                b.onClick.AddListener(delegate ()
                {
                    _owner.CloseAll();
                });
            }

            base.Init(owner);
        }

        public void SetButtons()  
        {
            Test();



            gameObject.SetActive(true);
        }

        public void UpdateButtonLevel()
        {
            foreach(Button_LevelUp b in _buttons)
            {
                Text t = b.transform.Find("Image").Find("Level").GetComponent<Text>();
                t.text = $"Lv.{b._ps._level}";
            }
        }

        public void Test()      // 강화 항목 무작위 선택, 버튼에 할당
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
