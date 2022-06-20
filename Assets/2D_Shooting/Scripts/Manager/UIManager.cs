using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ss
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager I;

        public LobbyUI _lobby;
        public PlayUI _play;
        void Awake()
        {
            I = this;
        }

        public void Init()
        {
            _lobby = transform.Find("Lobby").GetComponent<LobbyUI>();
            if (_lobby.gameObject.activeSelf == false)
                _lobby.gameObject.SetActive(true);
            _lobby.Init();
            
            _play = transform.Find("Play").GetComponent<PlayUI>();
            _play.Init();
        }

        public void BackToLobby()
        {
            _play.gameObject.SetActive(false);
            _lobby.CloseAll();
            _lobby.gameObject.SetActive(true);
        }

        public void GameStart(bool b = true)
        {
            if (b == true)
                _play.GameStart();
            else
                _play.TutorialStart();

            _play.gameObject.SetActive(true);
        }
    }
}
