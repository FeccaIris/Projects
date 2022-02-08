using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{
    public class Player : MonoBehaviour
    {
        public static Player I;

        public GameObject _unit;

        Rigidbody2D _rgd;

        private void Awake()
        {
            I = this;
        }
        private void Start()
        {
            _rgd = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            transform.position = _unit.transform.position;
        }
    }

}
