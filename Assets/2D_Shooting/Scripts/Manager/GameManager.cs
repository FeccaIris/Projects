using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ss
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager I;
        public Player _player;

        void Awake()
        {
            I = this;
        }

        void Start()
        {
            _player = FindObjectOfType<Player>(); // 프리팹 로드로 추후 변경
            _player.Init();

            UIManager.I.Init();
        }
    }
}
