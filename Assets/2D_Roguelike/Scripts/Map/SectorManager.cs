using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RL
{
    public class SectorManager : MonoBehaviour
    {
        public static SectorManager I;

        public List<Sector> _sectorL;
        public List<Sector> _emptyL;

        private void Awake()
        {
            I = this;
        }
        private void Start()
        {
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
            foreach(Sector s in _sectorL)
            {
                s.TurnOnCol();
                yield return null;
            }
        }

    }

}
