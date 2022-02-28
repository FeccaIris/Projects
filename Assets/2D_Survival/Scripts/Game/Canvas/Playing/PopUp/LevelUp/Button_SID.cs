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
                _owner.ReadyRF(_ps);           // 강화 준비 (무작위 선택지)
                _owner.TurnOnIndex(false);
            });

            gameObject.SetActive(false);
        }
        public void Set(PlayerSkill ps)
        {
            _ps = ps;
            gameObject.SetActive(true);
            _text.text = (_ps._index + 1).ToString() + "번 스킬" + "\n\n" + "코스트 : " + _ps._cost.ToString();
        }
    }
}
