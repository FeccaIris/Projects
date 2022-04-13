using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{
    public class SelectSkill : MonoBehaviour
    {
        List<Button_SelectSkill> _buttons = new List<Button_SelectSkill>();

        public int _startCount = 0;

        public void Init()
        {
            _buttons.AddRange(transform.Find("Buttons").GetComponentsInChildren<Button_SelectSkill>());
            foreach(Button_SelectSkill b in _buttons)
            {
                b.Init();

                b.onClick.AddListener(delegate ()
                {
                    _startCount++;
                    if(_startCount >= 3)
                        GameManager.I.GameStart();
                    b.gameObject.SetActive(false);
                });
            }

            gameObject.SetActive(true);
        }
    }
}
