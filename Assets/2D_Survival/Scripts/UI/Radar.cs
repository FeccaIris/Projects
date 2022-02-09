using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Radar : MonoBehaviour
    {
        public List<Enemy> _enemyList;
        
        GameObject _arrow;
        Transform _player;

        private void Start()
        {
            _arrow = transform.GetChild(0).gameObject;
            _player = Player.I.transform;

            _arrow.SetActive(false);

            if (_enemyList.Count > 0)
            {
                foreach (Enemy e in _enemyList)
                {
                    GameObject copy = Instantiate(_arrow);
                    copy.transform.parent = transform;
                    copy.transform.localPosition = _arrow.transform.localPosition;
                    copy.transform.localScale = _arrow.transform.localScale;
                    RadarArrow ra = copy.GetComponent<RadarArrow>();
                    ra._target = e.transform;
                    ra._player = Player.I.transform;
                    copy.SetActive(true);
                }
            }
        }
        private void Update()
        {
            
        }

    }
}
