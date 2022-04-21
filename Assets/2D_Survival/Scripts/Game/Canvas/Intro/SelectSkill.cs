using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{
    public class SelectSkill : MonoBehaviour
    {
        Button_SelectSkill _random;

        public void Init()
        {
            _random = transform.Find("Buttons").Find("Random").GetComponent<Button_SelectSkill>();
            _random.onClick.AddListener(delegate ()
            {

            });

            gameObject.SetActive(true);
        }
    }
}
