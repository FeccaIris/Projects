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

        public GameObject _icon_1;
        public Text _lv_1;
        public GameObject _icon_2;
        public Text _lv_2;
        public GameObject _icon_3;
        public Text _lv_3;

        List<Text> _lvList = new List<Text>();

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
            _icon_1 = play.Find("UI").Find("1").gameObject;
            _icon_2 = play.Find("UI").Find("2").gameObject;
            _icon_3 = play.Find("UI").Find("3").gameObject;

            _icon_2.SetActive(false);
            _icon_3.SetActive(false);

            _lv_1 = _icon_1.transform.Find("Text").GetComponent<Text>();
            _lv_2 = _icon_2.transform.Find("Text").GetComponent<Text>();
            _lv_3 = _icon_3.transform.Find("Text").GetComponent<Text>();

            _lv_1.text = "Lv.1";
            _lv_2.text = "Lv.1";
            _lv_3.text = "Lv.1";

            Text[] arr = { _lv_1, _lv_2, _lv_3 };
            _lvList.AddRange(arr);

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

        public void GameOver(bool clear = false)
        {
            _playing.SetActive(false);
            _gameOver.Show(clear);
        }

        public void LevelUP()
        {
            _lvUp.LevelUP();
        }
        public void AcquireSecond()
        {
            // 정면
            _icon_2.SetActive(true);
            _lvUp.AcquireNew(2);    // UI
            SkillManager.I.AcquireNew(hasT: false);
            SkillManager.I.SetAndActivate(SkillManager.I._skList[1], ea: 7, rch: 20.0f, cool: 2.0f, dmg: 1, interval: 0.1f, size: 5, pierce: 500, spd: 150.0f);
        }
        public void AcquireThird()
        {
            // 무작위
            _icon_3.SetActive(true);
            _lvUp.AcquireNew(3);
            SkillManager.I.AcquireNew(hasT: false);
            SkillManager.I.SetAndActivate(SkillManager.I._skList[2], ea: 70, dmg: 10, interval: 0.05f, spd: 250.0f, size: 1.5f, cool: 4.0f);
        }

        public void UpdateIconLevel(int id, int level)
        {
            _lvList[id].text = $"Lv.{level}";
        }
    }
}
