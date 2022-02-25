using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace s
{

    public class Lobby : MonoBehaviour
    {
        public List<Button> _btnList;

        public void Init()
        {
            _btnList = new List<Button>();
            _btnList.AddRange(transform.Find("1").Find("Buttons").GetComponentsInChildren<Button>());

            foreach(Button b in _btnList)
            {
                switch (b.name)
                {
                    case "Quit":
                        {
                            b.onClick.AddListener(delegate()
                            {
                                Application.Quit();
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
