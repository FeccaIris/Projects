using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{
    public enum Category
    {
        COOL,
        EA,
        MAINTAIN,
        REACH,
        SPEED,
        PIERCE,
        INTERVAL,
    }


    public class ThirdTab : LevelUpPU
    {
        public PlayerSkill _ps;

        List<Button> _buttons;

        public override void Init(LevelUp owner)
        {
            Button[] arr = transform.Find("Buttons").GetComponentsInChildren<Button>();
            _buttons = new List<Button>(arr);

            foreach(Button b in _buttons)
            {
                b.onClick.AddListener(delegate ()
                {
                    _owner.CloseAll();
                });
            }
            base.Init(owner);
        }

        public void ReadyForRF(PlayerSkill ps)
        {
            _ps = ps;
            List<Category> cat = new List<Category> { Category.COOL, Category.MAINTAIN };

            if(_ps != null)
            {
                if (_ps._isProjectile == true)
                {
                    cat.Add(Category.PIERCE);
                }
                else
                {
                    cat.Add(Category.INTERVAL);
                }
            }
        }
    }
}
