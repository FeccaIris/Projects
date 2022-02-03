using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RL
{

    public class MapModule : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.tag.Equals("Player"))
            {
                SectorManager.I.MoveToCenter(transform);
            }
        }

        public void FillEmpty()
        {
            //transform.position = ºó ±¸¿ª;
        }
    }

}