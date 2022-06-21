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

            _desc.text = "수행할 훈련을 선택하세요.";
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
                                _desc.text = "마우스 방향을 향해 이동합니다.";
                                _detail.text = "가속 : 왼쪽 클릭 / 감속 : C \n대쉬 : 더블 클릭";
                            });
                            break;
                        }
                    case "Shoot":
                        {
                            b.onClick.AddListener(delegate ()
                            {
                                _desc.text = "마우스 방향을 향해 사격합니다.";
                                _detail.text = "사격 : SpaceBar";
                            });
                            break;
                        }
                    case "Melee":
                        {
                            b.onClick.AddListener(delegate ()
                            {
                                _desc.text = "마우스 방향을 향해 공격합니다.";
                                _detail.text = "근접 공격 : A";
                            });
                            break;
                        }
                    case "Guard":
                        {
                            b.onClick.AddListener(delegate ()
                            {
                                _desc.text = "마우스 방향의 공격을 막습니다.";
                                _detail.text = "방어 : D";
                            });
                            break;
                        }
                    case "Return":
                        {
                            b.onClick.AddListener(delegate ()
                            {
                                _desc.text = "수행할 훈련을 선택하세요.";
                                _detail.text = "";
                                GameManager.I.PlayingNow(false);
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
