using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{

    public class Button_SID : Button
    {
        public PU_LevelUp _owner;

        public PlayerSkill _ps = null;

        public Text _text;

        public void Init(PU_LevelUp owner)
        {
            _owner = owner;

            _text = transform.Find("Text").GetComponent<Text>();

            onClick.AddListener(delegate ()
            {

                _owner.TurnOnIndex(false);
            });

            gameObject.SetActive(false);
        }
        public void Set(PlayerSkill ps)
        {
            _ps = ps;
            gameObject.SetActive(true);
            _text.text = _ps._index.ToString() + "\n" + _ps._cost.ToString();
        }
    }
}
