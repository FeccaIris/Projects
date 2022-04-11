using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{

    public class Expbar : MonoBehaviour
    {
        public Image _fill;

        public void Init()
        {
            _fill = transform.Find("Fill").GetComponent<Image>();
        }
    }
}
