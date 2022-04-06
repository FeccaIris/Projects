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
        SIZE,
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
                    b._ps.SkillReinforce(b._cat);

                    _owner.CloseAll();
                });
            }

            base.Init(owner);
        }

        public void SetButton(PlayerSkill ps)  
        {
            foreach(Button_LevelUp b in _buttons)
            {
                if(ps._index == b._index)
                {
                    b._ps = ps;
                    break;
                }
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

        public void ReadyReinforce()      // 강화 항목 무작위 선택, 버튼에 할당
        {
            int normal;
            normal = Random.Range(0, 3);

            foreach(Button_LevelUp b in _buttons)
            {
                if(b._index == normal)
                {
                    Test2(b, true);
                }
                else
                {
                    Test2(b);
                }
            }

            gameObject.SetActive(true);
        }
        public void Test2(Button_LevelUp b, bool normal = false)
        {
            PlayerSkill ps = b._ps;

            List<Category> cats = new List<Category> { Category.DAMAGE, Category.SIZE };

            if (ps._hasCool)
            {
                cats.Add(Category.COOL);
            }
            if (ps._isProjectile)
            {
                cats.Add(Category.PIERCE);
                cats.Add(Category.SPEED);
                cats.Add(Category.EA);
                if (ps._ea > 1)
                {
                    cats.Add(Category.INTERVAL);
                }
            }
            else
            {
                cats.Add(Category.MAINTAIN);
                if (ps._doesStay)
                {
                    cats.Add(Category.INTERVAL);
                }
            }

            //ps.SkillReinforce(cat);
        }
    }
}
