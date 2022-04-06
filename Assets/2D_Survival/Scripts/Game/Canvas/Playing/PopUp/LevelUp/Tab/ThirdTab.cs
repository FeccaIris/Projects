using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{
    public class ThirdTab : LevelUpPU
    {
        public PlayerSkill _ps;

        List<Button_LevelUp> _buttons;

        public override void Init(LevelUp owner)
        {
            Button_LevelUp[] arr = transform.Find("Buttons").GetComponentsInChildren<Button_LevelUp>();
            _buttons = new List<Button_LevelUp>(arr);

            foreach(Button_LevelUp b in _buttons)
            {
                b.onClick.AddListener(delegate ()
                {
                    _ps.SkillReinforce(b._cat);
                    //_owner._secondTab.UpdateButtonLevel();

                    _owner.CloseAll();
                });
            }
            base.Init(owner);
        }

        public void ReadyForRF(PlayerSkill ps)
        {
            _ps = ps;
            List<Category> cat = new List<Category> { Category.COOL, Category.DAMAGE, Category.INTERVAL };

            if(_ps != null)
            {
                if (_ps._isProjectile == true)
                {
                    cat.Add(Category.PIERCE);
                    cat.Add(Category.SPEED);
                    cat.Add(Category.EA);
                }
                else
                {
                    cat.Add(Category.MAINTAIN);
                }
            }

            int r1 = Random.Range(0, cat.Count);
            int r2, r3;

            while (true)
            {
                r2 = Random.Range(0, cat.Count);
                if (r1 != r2) break;
            }
            while (true)
            {
                r3 = Random.Range(0, cat.Count);
                if (r3 != r2 && r3 != r1) break;
            }

            Category c1, c2, c3;
            c1 = cat[r1];
            c2 = cat[r2];
            c3 = cat[r3];
            List<Category> list = new List<Category> { c1, c2, c3 };

            for(int i = 0; i < _buttons.Count; i++)
            {
                _buttons[i]._cat = list[i];

                Text t = _buttons[i].transform.Find("Text").GetComponent<Text>();
                //t.text = list[i].ToString();
                switch (list[i])
                {
                    case Category.DAMAGE:
                        {
                            t.text = "���ݷ� ����";
                            break;
                        }
                    case Category.COOL:
                        {
                            t.text = "��Ÿ�� ����";
                            break;
                        }
                    case Category.EA:
                        {
                            t.text = "���� ����";
                            break;
                        }
                    case Category.INTERVAL:
                        {
                            t.text = "���ذ��� ����";
                            break;
                        }
                    case Category.MAINTAIN:
                        {
                            t.text = "�����ð� ����";
                            break;
                        }
                    case Category.PIERCE:
                        {
                            t.text = "����� ����";
                            break;
                        }
                    case Category.REACH:
                        {
                            t.text = "��Ÿ� ����";
                            break;
                        }
                    case Category.SPEED:
                        {
                            t.text = "�ӵ� ����";
                            break;
                        }
                }
            }

        }
    }
}
