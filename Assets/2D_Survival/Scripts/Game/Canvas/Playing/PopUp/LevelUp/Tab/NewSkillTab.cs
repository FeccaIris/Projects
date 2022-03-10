using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{
    public class NewSkillTab : LevelUpPU
    {
        Button _confirm;

        public Text _name;
        public Text _desc;

        public override void Init(LevelUp owner)
        {
            _confirm = transform.Find("Button").GetComponent<Button>();
            _name = transform.Find("Name").GetComponent<Text>();
            _desc = transform.Find("Desc").GetComponent<Text>();

            _confirm.onClick.AddListener(delegate ()
            {
                _owner.CloseAll();
            });
            base.Init(owner);
        }

        public void Show(int id)
        {
            if(id == 2)
            {
                _name.text = "자동 사격";
                _desc.text = "가장 가까운 적을 향해 사격합니다.";
                gameObject.SetActive(true);
            }
            else if(id == 3)
            {
                _name.text = "난사";
                _desc.text = "다수의 탄환을 무작위 방향으로 사격합니다.";
                gameObject.SetActive(true);
            }
        }
    }
}

