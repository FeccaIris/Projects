using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager I;

        [SerializeField] List<Enemy> _enemys;

        private void Awake()
        {
            I = this;
        }
    }
}
