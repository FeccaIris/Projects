using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RL
{

    public class Sector : MonoBehaviour
    {
        BoxCollider2D _col;

        private void Start()
        {
            SwitchCol(false);
            _col = GetComponent<BoxCollider2D>();
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            
        }
        private void FixedUpdate()
        {
            if (_col != null)
            {
                if (_col.enabled == true)
                    Invoke("TurnOff", 0.2f);
            }
        }

        public void SwitchCol(bool tf)
        {
            if (_col.enabled != tf) 
                _col.enabled = tf;

        }
        public void TurnOff()
        {
            SwitchCol(false);
        }
    }

}
