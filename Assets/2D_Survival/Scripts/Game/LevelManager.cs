using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class LevelManager : MonoBehaviour
    {
        public static LevelManager I;

        int _lv;
        int _exp;

        public AnimationCurve _curve;
        int _lvMax = 200;
        int _expMax = 100000;

        void Awake()
        {
            I = this;
        }

        public void Init()
        {
            _lv = 1;
            _exp = 0;
        }

        public void GetExp(int exp)
        {
            _exp += exp;

            int cE, nE, need;
            Calc(out cE, out nE, out need);

            if(_exp >= need)
            {
                int exceed = _exp - need;
                Time.timeScale = 0;


            }
        }
        void Calc(out int curExp, out int nextExp, out int needExp)     // 필요 경험치 계산 -> 레벨 참조
        {
            int curLv = _lv;
            int nextLv = curLv + 1;

            curExp = (int) _curve.Evaluate(curLv / _lvMax);
            nextExp = (int) _curve.Evaluate(nextLv / _lvMax);
            needExp = nextExp - curExp;
        }
    }
}
