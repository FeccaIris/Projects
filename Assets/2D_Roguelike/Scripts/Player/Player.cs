using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RL
{

    public class Player : MonoBehaviour
    {
        public static Player I;

        private void Awake()
        {
            I = this;
        }
    }

}
