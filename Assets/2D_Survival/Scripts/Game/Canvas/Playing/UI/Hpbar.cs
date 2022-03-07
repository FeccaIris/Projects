using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{
    public class Hpbar : MonoBehaviour
    {
        public Image _fill;

        //float _offset = -25.0f;

        public void Init()
        {
            _fill = transform.Find("Fill").GetComponent<Image>();
            /*
            if (Player.I != null)
            {
                Vector2 pos = Camera.main.WorldToScreenPoint(Player.I.transform.position);
                transform.position = new Vector2(pos.x, pos.y + _offset);
            }*/
        }
    }
}
