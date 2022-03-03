using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ss
{

    public class Lobby_UI : MonoBehaviour
    {
        Lobby _owner;
        List<Button> _buttons;

        public virtual void Init(Lobby owner)
        {
            _owner = owner;
            _owner._uiList.Add(this);

            _buttons = new List<Button>(transform.Find("Buttons").GetComponentsInChildren<Button>(true));

            foreach (Button b in _buttons)
            {
                switch (b.name)
                {
                    case "Return":
                        {
                            b.onClick.AddListener(delegate ()
                            {
                                UIManager.I._lobby.CloseAll();
                            });

                            break;
                        }
                    case "Start":
                        {
                            b.onClick.AddListener(delegate ()
                            {
                                UIManager.I._lobby.CloseAll(true);
                                UIManager.I._lobby._selectUnit.gameObject.SetActive(true);
                            });
                        }
                        break;
                    default:
                        break;
                }
            }

            gameObject.SetActive(false);
        }
    }
}
