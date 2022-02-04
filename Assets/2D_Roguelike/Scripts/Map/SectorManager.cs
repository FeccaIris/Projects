using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RL
{
    public class SectorManager : MonoBehaviour
    {
        public static SectorManager I;

        MapManager _mm;
        List<Sector> _sectorL;
        List<Sector> _emptyL;

        private void Awake()
        {
            I = this;
        }
        private void Start()
        {
            _mm = MapManager.I;
            Sector[] arr = GetComponentsInChildren<Sector>();
            _sectorL = new List<Sector>(arr);
            _emptyL = new List<Sector>();
        }

        public void MoveToCenter(Transform center)
        {
            if (transform.position == center.position)
                return;

            transform.position = center.position;
            StartCoroutine(CheckMap());
        }

        IEnumerator CheckMap()
        {
            _emptyL = new List<Sector>(_sectorL);
            _mm.RefillList();

            foreach (Sector s in _sectorL)
            {
                s.TurnOnCol();
                yield return null;
            }

            _mm.FillEmpty(_emptyL);
        }

        public void ModifyList(Sector s)
        {
            foreach(Sector sc in _emptyL)
            {
                if (sc == s)
                {
                    _emptyL.Remove(s);
                    break;
                }
            }
        }

    }
}
