using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace s
{

    public class GameManager : MonoBehaviour
    {
        public static GameManager I;

        void Awake()
        {
            I = this;
        }

        void Start()
        {
            UIManager.I.Init();
        }
    }
}
