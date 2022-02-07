using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
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

        public void Move(Transform t)
        {
            transform.position = t.position;
        }
    }
}