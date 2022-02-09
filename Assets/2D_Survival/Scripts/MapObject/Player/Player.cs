using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{
    public class Player : Unit
    {
        public static Player I;

        public GameObject _unit;
        public Enemy _nearest;

        private void Awake()
        {
            I = this;
        }
        protected override void Start()
        {
            base.Start();

            _unit = transform.Find("Unit").gameObject;
        }
        protected override void Update()
        {
            base.Update();
        }
        protected override void FixedUpdate()
        {
            base.FixedUpdate();
        }
    }

}
