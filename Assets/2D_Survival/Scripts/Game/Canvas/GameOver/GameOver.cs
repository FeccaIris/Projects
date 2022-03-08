using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

namespace SV
{

    public class GameOver : MonoBehaviour
    {
        public Button _restart;

        public Text _time;
        public Text _kills;
        public Text _level;

        public void Init()
        {
            _time = transform.Find("Records").transform.Find("Time").GetComponent<Text>();
            _kills = transform.Find("Records").transform.Find("Kills").GetComponent<Text>();
            _level = transform.Find("Records").transform.Find("Level").GetComponent<Text>();

            Button b = transform.Find("Restart").GetComponent<Button>();
            b.onClick.AddListener(delegate ()
            {
                SceneManager.LoadScene("Loading");
            });

            gameObject.SetActive(false);
        }

        public void Show()
        {
            float time = GameManager.I._gameTime;
            TimeSpan t = new TimeSpan(0, 0, (int)time);

            if (t.Hours >= 1)
                _time.text = t.Hours + " : " + t.Minutes + " : " + t.Seconds;
            else
                _time.text = t.Minutes + " : " + t.Seconds;
            _kills.text = GameManager.I._kills.ToString();
            _level.text = LevelManager.I.Level.ToString();

            gameObject.SetActive(true);
        }
    }
}
