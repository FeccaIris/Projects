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
                float time = GameManager.I._gameTime;
                TimeSpan t = new TimeSpan(0, 0, (int)time);

                if (t.Hours >= 1)
                    _gameTime.text = t.Hours + " : " + t.Minutes + " : " + t.Seconds;
                else
                    _gameTime.text = t.Minutes + " : " + t.Seconds;
            }
        }

        public void GameOver()
        {
            _playing.SetActive(false);
            _gameOver.Show();
        }

        public void LevelUP()
        {
            _lvUp.LevelUP();
        }
        public void AcquireSecond()
        {
            // 정면
            _lvUp.AcquireNew(2);
            SkillManager.I.AcquireNew(hasT: true);
            SkillManager.I.SetSkill(SkillManager.I._skList[1], rch: 20.0f, cool: 2.0f, dmg: 1);
        }
        public void AcquireThird()
        {
            // 무작위
            _lvUp.AcquireNew(3);
            SkillManager.I.AcquireNew(hasT: false);
            SkillManager.I.SetSkill(SkillManager.I._skList[2]);
        }
    }
}
