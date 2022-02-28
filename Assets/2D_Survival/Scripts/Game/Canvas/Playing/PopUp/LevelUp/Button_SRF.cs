using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{
    public class Button_SRF : Button
    {
        public PU_LevelUp _owner;

        public Category _category;

        Text _text;

        public void Init(PU_LevelUp owner)
        {
            _owner = owner;
            _text = GetComponentInChildren<Text>();

            onClick.AddListener(delegate ()
            {
                _owner.OnEnd();
                _owner._rftable.Reinforce(_category);
            });
        }

        public void SetCategory(Category cat)
        {
            _category = cat;
            _text.text = cat.ToString();
        }
    }
}
