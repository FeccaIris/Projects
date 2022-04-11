using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{
    public class SelectSkill : MonoBehaviour
    {
        List<Button> _buttons = new List<Button>();

        public void Init()
        {
            _buttons.AddRange(transform.Find("Buttons").GetComponentsInChildren<Button>());
            foreach(Button b in _buttons)
            {
                b.onClick.AddListener(delegate ()
                {
                    GameManager.I.GameStart();
                });
            }
        }
    }
}
