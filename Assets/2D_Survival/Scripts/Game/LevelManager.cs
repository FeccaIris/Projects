using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class LevelManager : MonoBehaviour
    {
        public static LevelManager I;

        int _lvCur;
        int _expCur;

        public AnimationCurve _expCurve;
        int _lvMax = 200;
        int _expMax = 100000;

        void Awake()
        {
            I = this;
        }

        public void Init()
        {
            _lvCur = 1;
            _expCur = 0;
        }

        public void GetExp(int exp)
        {
            _expCur += exp;

            Calc();
        }
        void Calc()     // �ʿ� ����ġ ��� -> ���� ����
        {
            int curLv = _lvCur;
            int nextLv = curLv + 1;
        }
    }
}
