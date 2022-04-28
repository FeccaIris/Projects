using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{
    public class SelectSkill : MonoBehaviour
    {
        Button _start;

        public void Init()
        {
            _start = transform.Find("Buttons").Find("Start").GetComponent<Button>();
            _start.gameObject.SetActive(false);

            Button_SelectSkill roll;
            roll = transform.Find("Buttons").Find("Roll").GetComponent<Button_SelectSkill>();
            roll.onClick.AddListener(delegate ()
            {
                for(int i = 0; i < 3; i++)
                {

                }

                GameManager.I.GameStart();
            });

            gameObject.SetActive(true);
        }
    }
}
