using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class MapManager : MonoBehaviour
    {
        public static MapManager I;

        List<MapModule> _mapL;
        List<MapModule> _outL;

        private void Awake()
        {
            I = this;
        }
        private void Start()
        {
            MapModule[] arr = GetComponentsInChildren<MapModule>();
            _mapL = new List<MapModule>(arr);
            _outL = new List<MapModule>();
        }

        public void RefillList()
        {
            _outL = new List<MapModule>(_mapL);
        }

        public void ModifyList(MapModule m)
        {
            foreach(MapModule md in _outL)
            {
                if(md == m)
                {
                    _outL.Remove(m);
                    break;
                }
            }
        }

        public void FillEmpty(List<Sector> list)
        {
            int c = list.Count;
            Transform t;
            MapModule m;

            for(int i = 0; i < c; i++)
            {
                m = _outL[i];
                t = list[i].transform;

                m.Move(t);
            }
        }
    }

}

