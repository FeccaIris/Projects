using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{
    public class Player : MonoBehaviour
    {
        public static Player I;

        public GameObject _unit;

        private void Awake()
        {
            I = this;
        }
        private void Start()
        {

        }
    }

}
