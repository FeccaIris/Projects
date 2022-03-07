using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ss
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
            Player.I.Init();


            UIManager.I.Init();
        }
    }
}
