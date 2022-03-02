using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{
    public class Button_SRF : Button
    {
        public PU_LevelUp _owner;
        public PlayerSkill _ps;

        public Category _category;

        Text _text;

        public void Init(PU_LevelUp owner)
        {
            _owner = owner;
            _text = GetComponentInChildren<Text>();

            onClick.AddListener(delegate ()
            {
                _owner.OnEnd();
                _owner._rftable.Reinforce(_ps, _category);
            });
        }

        public void SetCategory(Category cat, PlayerSkill ps)
        {
            _ps = ps;
            _category = cat;
            _text.text = cat.ToString();
        }
    }
}
