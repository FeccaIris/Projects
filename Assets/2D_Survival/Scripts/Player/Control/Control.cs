using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{
    public class Control : MonoBehaviour
    {
        protected Rigidbody2D _rgd;
        protected Player _player;

        protected virtual void Start()
        {
            _rgd = GetComponent<Rigidbody2D>();
            _player = Player.I;
        }
        protected virtual void Update()
        {

        }
        protected virtual void FixedUpdate()
        {

        }
    }
}
