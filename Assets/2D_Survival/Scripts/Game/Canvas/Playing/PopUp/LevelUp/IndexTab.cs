using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{

    public class IndexTab : LevelUpPU
    {
        public List<Button_LevelUp> _idButtons;
        public List<Button> _buttons;

        public override void Init(LevelUp owner)
        {
            _idButtons = new List<Button_LevelUp>(transform.Find("IDB").GetComponentsInChildren<Button_LevelUp>());
            _buttons = new List<Button>(transform.Find("Else").GetComponentsInChildren<Button>());

            foreach(Button_LevelUp b in _idButtons)
            {
                b.onClick.AddListener(delegate ()
                {
                    if(b._ps != null)
                        _owner.ReadyForReinforce(b._ps);
                    _owner.TurnOnOff(off: this, on: _owner._reinforceT);
                });

                b.gameObject.SetActive(false);
            }

            foreach(Button b in _buttons)
            {
                switch (b.name)
                {
                    case "CostUp":
                        {
                            //코스트 미구현
                            b.onClick.AddListener(delegate()
                            {
                                _owner.CloseAll();
                            });
                            break;
                        }
                    case "NewSkill":
                        {
                            // 스킬 설정 UI 미구현
                            // 0315 => 영역형 및 투사체 구별까지
                            b.onClick.AddListener(delegate()
                            {
                                SkillManager.I.AcquireNew();
                                _owner.CloseAll();
                            });
                            break;
                        }
                    case "DmgUp":
                        {
                            b.onClick.AddListener(delegate ()
                            {
                                foreach(PlayerSkill k in SkillManager.I._skList)
                                {
                                    k._dmg += 1;
                                    Debug.Log(k._dmg);
                                    _owner.CloseAll();
                                }
                            });

                            break;
                        }
                    default:
                        break;
                }
            }

            base.Init(owner);
        }

        public void SetIndexButton()
        {
            Debug.Assert(_idButtons.Count >= SkillManager.I._skList.Count);

            for(int i = 0; i < SkillManager.I._skList.Count; i++)
            {
                Button_LevelUp b = _idButtons[i];
                PlayerSkill ps = SkillManager.I._skList[i];

                if (b != null)
                    b._ps = ps;

                b.gameObject.SetActive(true);
            }
        }
    }
}
