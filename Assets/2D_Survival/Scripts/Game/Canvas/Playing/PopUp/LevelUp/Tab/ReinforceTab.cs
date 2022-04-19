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

    public class ReinforceTab : LevelUpPU
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
                b.Init();

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
        public void ReadyReinforce()      // 강화 항목 무작위 선택, 버튼에 할당
        {
            foreach(Button_LevelUp b in _buttons)
            {
                FixCategory(b);
            }

            gameObject.SetActive(true);
        }

        // 강화탭 버튼 업데이트
        public void FixCategory(Button_LevelUp b)  // foreach 3회 호출
        {
            PlayerSkill ps = b._ps;

            List<KeyValuePair<Category, int>> list = new List<KeyValuePair<Category, int>>(ps._catLevels);
            List<Category> cats = new List<Category> { Category.COOL, Category.DAMAGE, Category.SIZE };

            if (ps._isProjectile)
            {
                if(ps._isRandom != true)
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
                if (ps._isRandom == true || ps._isStatic == true)
                {
                    cats.Add(Category.MAINTAIN);
                    cats.Add(Category.INTERVAL);
                }
            }

            foreach (KeyValuePair<Category, int> kv in list)
            {
                if(kv.Value > 3)
                {
                    cats.Remove(kv.Key);
                }
            }

            /// 랜덤 항목 선택
            int random = 0;
            if (cats.Count > 0)
            {
                random = Random.Range(0, cats.Count);
                b._cat = cats[random];
                b._lv.text = $"Lv. {ps._catLevels[cats[random]].ToString()}";

                switch (cats[random])
                {
                    case Category.DAMAGE:
                        {
                            b._txt.text = "공격력 증가";
                            break;
                        }
                    case Category.SIZE:
                        {
                            b._txt.text = "크기 증가";
                            break;
                        }
                    case Category.COOL:
                        {
                            b._txt.text = "쿨타임 감소";
                            break;
                        }
                    case Category.PIERCE:
                        {
                            b._txt.text = "관통횟수 증가";
                            break;
                        }
                    case Category.SPEED:
                        {
                            b._txt.text = "속도 증가";
                            break;
                        }
                    case Category.EA:
                        {
                            b._txt.text = "개수 증가";
                            break;
                        }
                    case Category.INTERVAL:
                        {
                            b._txt.text = "발동간격 감소";
                            break;
                        }
                    case Category.MAINTAIN:
                        {
                            b._txt.text = "지속시간 증가";
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
            else
            {
                b.interactable = false;
                b._txt.text = "Master";
                b._lv.text = $"";
            }
        }
    }
}
