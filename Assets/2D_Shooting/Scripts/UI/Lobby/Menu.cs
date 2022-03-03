using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ss
{

    public class Menu : MonoBehaviour
    {
        List<Button> _buttons;

        public void Init()
        {
            _buttons = new List<Button>(transform.Find("Buttons").GetComponentsInChildren<Button>(true));

            foreach (Button b in _buttons)
            {
                switch (b.name)
                {
                    case "Survival":
                        {
                            b.onClick.AddListener(delegate ()
                            {
                                UIManager.I._lobby._survival.gameObject.SetActive(true);
                            });
                            break;
                        }
                    case "Infinite":
                        {
                            b.onClick.AddListener(delegate ()
                            {
                                UIManager.I._lobby._infinite.gameObject.SetActive(true);
                            });
                            break;
                        }
                    case "Option":
                        {
                            b.onClick.AddListener(delegate ()
                            {
                                UIManager.I._lobby._option.gameObject.SetActive(true);
                            });
                            break;
                        }
                    case "Credit":
                        {
                            b.onClick.AddListener(delegate ()
                            {
                                UIManager.I._lobby._credit.gameObject.SetActive(true);
                            });
                            break;
                        }
                    case "Quit":
                        {
                            b.onClick.AddListener(delegate ()
                            {
                                Application.Quit();
                                Debug.Log("quit");
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
