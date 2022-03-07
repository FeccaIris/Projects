using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{

    public class UIManager : MonoBehaviour
    {
        public static UIManager I;

        public GameObject _playing;
        public GameOver _gameOver;

        public Hpbar _hpB;
        public Expbar _expB;
        public Text _gameTime;

        public LevelUp _lvUp;


        void Awake()
        {
            I = this;
        }

        public void Init()
        {
            _playing = transform.Find("Playing").gameObject;
            _gameOver = transform.Find("GameOver").GetComponent<GameOver>();

            _gameOver.Init();

            Transform play = transform.Find("Playing");

            _hpB = play.Find("UI").Find("Hpbar").GetComponent<Hpbar>();
            _expB = play.Find("UI").Find("Expbar").GetComponent<Expbar>();
            _gameTime = play.Find("UI").Find("GameTime").GetComponent<Text>();

            _lvUp = play.Find("PopUp").Find("LevelUp").GetComponent<LevelUp>();
            
            _hpB.Init();
            _expB.Init();
            
            _lvUp.Init();
        }

        public void FixedUpdate()
        {
            if (GameManager.I._playing)
            {
                int temp = (int)GameManager.I._gameTime / 60;
                string m = temp.ToString();

                temp = (int)GameManager.I._gameTime % 60;
                string s = temp.ToString();

                _gameTime.text = m + ":" + s;
            }
        }

        public void GameOver()
        {
            _playing.SetActive(false);
            _gameOver.gameObject.SetActive(true);
        }

        public void LevelUP()
        {
            _lvUp.LevelUP();
        }
    }
}
