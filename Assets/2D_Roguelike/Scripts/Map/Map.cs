using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RL
{

    public class Map : MonoBehaviour
    {
        public static Map I;

        private void Awake()
        {
            I = this;
        }
    }

}

