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
        List<Button> _buttons;

        public Button _restart;

        public Text _title;
        public Text _time;
        public Text _kills;
        public Text _level;

        public void Init()
        {
            _title = transform.Find("Title").GetComponent<Text>();
            _time = transform.Find("Records").transform.Find("Time").GetComponent<Text>();
            _kills = transform.Find("Records").transform.Find("Kills").GetComponent<Text>();
            _level = transform.Find("Records").transform.Find("Level").GetComponent<Text>();

            Button[] arr = transform.Find("Buttons").GetComponentsInChildren<Button>();
            _buttons = new List<Button>(arr);

            foreach(Button b in _buttons)
            {
                switch (b.name)
                {
                    case "Restart":
                        {
                            b.onClick.AddListener(delegate ()
                            {
                                SceneManager.LoadScene("Playing");
                            });
                            break;
                        }
                    case "Quit":
                        {
                            b.onClick.AddListener(delegate ()
                            {
                                Application.Quit();
                            });
                            break;
                        }
                }
            }

            gameObject.SetActive(false);
        }

        public void Show(bool clear)
        {
            if (clear == true)
                _title.text = "Clear !!";

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
