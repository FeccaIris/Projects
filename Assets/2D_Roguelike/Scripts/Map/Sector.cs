using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RL
{

    public class Sector : MonoBehaviour
    {
        BoxCollider2D _col;
        SectorManager _sm;
        MapManager _mm;

        private void Start()
        {
            _col = GetComponent<BoxCollider2D>();
            TurnOffCol();
            _sm = SectorManager.I;
            _mm = MapManager.I;
        }
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag.Equals("Map"))
            {
                MapModule m = col.GetComponent<MapModule>();
                _mm.ModifyList(m);
                _sm.ModifyList(this);
            }
        }

        public void TurnOnCol()
        {
            if (_col.enabled == true)
                return;

            _col.enabled = true;
            Invoke("TurnOffCol", 0.2f);
        }
        public void TurnOffCol()
        {
            if (_col.enabled == false)
                return;

            _col.enabled = false;
        }
    }

}
