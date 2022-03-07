using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ss
{
    public class Tutorial : Play_Child
    {
        public List<Button> _buttons;

        Text _desc;
        Text _detail;

        public override void Init(PlayUI owner)
        {
            base.Init(owner);

            _desc = transform.Find("Panel").Find("Desc").GetComponent<Text>();
            _detail = transform.Find("Panel").Find("Detail").GetComponent<Text>();

            _desc.text = "������ �Ʒ��� �����ϼ���.";
            _detail.text = "";


            Button[] arr = transform.Find("Panel_Button").Find("Buttons").GetComponentsInChildren<Button>();

            _buttons = new List<Button>(arr);

            foreach(Button b in _buttons)
            {
                switch (b.name)
                {
                    case "Move":
                        {
                            b.onClick.AddListener(delegate ()
                            {
                                _desc.text = "���콺�� ������ ���ϰ� ���� Ŭ������ �����մϴ�.";
                                _detail.text = "�뽬 : ����Ŭ�� \n���� : C";
                            });
                            break;
                        }
                    case "Shoot":
                        {

                            break;
                        }
                    case "Melee":
                        {

                            break;
                        }
                    case "Guard":
                        {

                            break;
                        }
                    case "Return":
                        {
                            b.onClick.AddListener(delegate ()
                            {
                                UIManager.I.BackToLobby();
                            });
                            break;
                        }
                    default:
                        break;
                }
            }

        }
    }
}
