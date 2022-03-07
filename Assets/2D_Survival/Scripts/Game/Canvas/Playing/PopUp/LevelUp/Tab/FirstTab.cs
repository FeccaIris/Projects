using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{

    public class FirstTab : LevelUpPU
    {
        public List<Button> _buttons;

        public override void Init(LevelUp owner)
        {
            _buttons = new List<Button>(transform.Find("Buttons").GetComponentsInChildren<Button>());

            foreach(Button b in _buttons)
            {
                switch (b.name)
                {
                    case "Each":
                        {
                            b.onClick.AddListener(delegate ()
                            {
                                _owner.OnOff(_owner._secondTab, this);
                            });

                            break;
                        }
                    case "Common":
                        {
                            b.onClick.AddListener(delegate ()
                            {
                                foreach(PlayerSkill k in SkillManager.I._skList)
                                {
                                    k._dmg += 1;
                                    k._size *= 1.15f;
                                }

                                _owner.CloseAll();
                            });

                            break;
                        }
                    default:
                        break;
                }
            }

            base.Init(owner);
        }
    }
}
