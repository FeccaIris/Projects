using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ss
{

    public class UIManager : MonoBehaviour
    {
        public static UIManager I;

        public Lobby _lobby;

        void Awake()
        {
            I = this;
        }

        public void Init()
        {
            _lobby = transform.Find("Lobby").GetComponent<Lobby>();
            _lobby.Init();
        }
    }
}
