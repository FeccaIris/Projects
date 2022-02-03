using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RL
{

    public class MapManager : MonoBehaviour
    {
        public static MapManager I;

        private void Awake()
        {
            I = this;
        }
    }

}

