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

        SelectSkill _selectSkill;
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
            _selectSkill = transform.Find("SelectSkill").GetComponent<SelectSkill>();
            _playing = transform.Find("Playing").gameObject;
            _gameOver = transform.Find("GameOver").GetComponent<GameOver>();

            _selectSkill.Init();
            if(_playing.activeSelf == true)
                _playing.SetActive(false);
            _gameOver.Init();

            Transform play = transform.Find("Playing");
            Transform ui = play.Find("UI").Find("UI");

            if (play.Find("PopUp").gameObject?.activeSelf == false)
                play.Find("PopUp").gameObject.SetActive(true);

            _hpB = ui.Find("Hpbar").GetComponent<Hpbar>();

            _expB = ui.Find("Expbar").GetComponent<Expbar>();
            _gameTime = ui.Find("GameTime").GetComponent<Text>();
            _icon_1 = ui.Find("1").gameObject;
            _icon_2 = ui.Find("2").gameObject;
            _icon_3 = ui.Find("3").gameObject;

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
        public void GameStart()
        {
            if(_selectSkill.gameObject.activeSelf == true)
                _selectSkill.gameObject.SetActive(false);

            if (_gameOver.gameObject.activeSelf == true)
                _gameOver.gameObject.SetActive(false);

            _playing.SetActive(true);
        }
        public void FixedUpdate()
        {
            if (GameManager.I._isPlaying)
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

        public void UpdateIconLevel(PlayerSkill ps)
        {
            _lvList[ps._index].text = $"Lv.{ps._level}";
        }

        public void EndLevelUp()
        {
            _expB.gameObject.SetActive(false);
        }
    }
}
