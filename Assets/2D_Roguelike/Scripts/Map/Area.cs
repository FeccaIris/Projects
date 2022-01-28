using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RL
{
    public class Area : MonoBehaviour
    {
        public static Area I;

        List<Sector> _sectorL;

        private void Awake()
        {
            I = this;
        }
        private void Start()
        {
            Sector[] arr = GetComponentsInChildren<Sector>();
            _sectorL = new List<Sector>(arr);
        }
        private void FixedUpdate()
        {
            //transform.position = 플레이어 중심;
        }

        public void CheckEmpty()
        {
            foreach(Sector s in _sectorL)
            {
                s.SwitchCol(true);
            }
        }


    }

}
