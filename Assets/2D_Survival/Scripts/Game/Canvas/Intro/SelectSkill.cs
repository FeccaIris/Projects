using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{
    public class SelectSkill : MonoBehaviour
    {
        Button _start;
        Text _cTxt;
        int _count = 3;

        public Sprite _pj;
        public Sprite _area;

        public void Init()
        {
            _cTxt = transform.Find("Bg").Find("Count").GetComponent<Text>();
            _cTxt.text = _count.ToString();

            _start = transform.Find("Buttons").Find("Start").GetComponent<Button>();
            _start.onClick.AddListener(delegate ()
            {
                GameManager.I.GameStart();
            });
            _start.interactable = false;

            Button roll;
            roll = transform.Find("Buttons").Find("Roll").GetComponent<Button>();
            roll.onClick.AddListener(delegate ()
            {
                SkillManager.I.ClearAll();

                _count--;
                _cTxt.text = _count.ToString();
                if (_count <= 0)
                {
                    roll.interactable = false;
                }

                int r1 = Random.Range(0, 6);
                int r2 = Random.Range(0, 6);
                int r3 = Random.Range(0, 6);

                while (r2 == r1)
                {
                    r2 = Random.Range(0, 6);
                }

                Transform sk = transform.Find("Skills");
                Text t1 = sk.Find("1").Find("Panel").Find("Text").GetComponent<Text>();
                Text t2 = sk.Find("2").Find("Panel").Find("Text").GetComponent<Text>();
                Text t3 = sk.Find("3").Find("Panel").Find("Text").GetComponent<Text>();

                t1.text = SkillManager.I.AcquireRandom(r1);
                t2.text = SkillManager.I.AcquireRandom(r2);
                t3.text = SkillManager.I.AcquireRandom(r3);

                if (_start.interactable == false)
                {
                    Text t = roll.transform.Find("Text").GetComponent<Text>();
                    t.text = "Re-Roll";
                    _start.interactable = true;
                }
            });

            gameObject.SetActive(true);
        }
    }
}
