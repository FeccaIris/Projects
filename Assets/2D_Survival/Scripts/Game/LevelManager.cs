using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class LevelManager : MonoBehaviour
    {
        public static LevelManager I;

        public Expbar _expB;

        [SerializeField] int _lv = 1;
        [SerializeField] int _exp;
        [SerializeField] int _expNeed;

        public int Level { get { return _lv; } }

        public AnimationCurve _curve;
        int _lvMax = 200;
        int _expMax = 100000;

        void Awake()
        {
            I = this;
        }

        public void Init()
        {
            _expB = UIManager.I._expB;
            UpdateExpbar();

            _lv = 1;
            _exp = 0;
        }

        public void UpdateExpbar()
        {
            _expNeed = Calc(_lv);
            _expB._fill.fillAmount = (float)_exp / _expNeed;
        }

        public void GetExp(int exp)
        {
            _exp += exp;
            UpdateExpbar();

            CheckLevelUp();
        }
        public void CheckLevelUp()
        {
            if (_exp >= _expNeed)
            {
                LevelUp();

                UpdateExpbar();
            }
            else return;
        }
        int Calc(int lv)
        {
            int curLv = lv - 1;
            int nextLv = lv;

            float temp = _curve.Evaluate((float)curLv / _lvMax);
            int cur = (int)(_expMax * temp);

            temp = _curve.Evaluate((float)nextLv / _lvMax);
            int next = (int)(_expMax * temp);

            int need = next - cur;

            return need;
        }
        void LevelUp()
        {
            int exceed = _exp - _expNeed;
            Time.timeScale = 0;

            if (_lv == 10)
            {
                UIManager.I.AcquireThird();
            }
            else if (_lv == 5)
            {
                UIManager.I.AcquireSecond();
            }
            else
            {
                UIManager.I.LevelUP();
            }

            _lv++;
            _exp = exceed;
        }
    }
}
