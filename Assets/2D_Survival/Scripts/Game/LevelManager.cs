using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class LevelManager : MonoBehaviour
    {
        public static LevelManager I;

        public Expbar _expB;
        public bool TEST = true;
        [SerializeField] int _lv = 1;
        [SerializeField] int _exp;
        [SerializeField] int _expNeed;

        public int Level { get { return _lv; } }

        public AnimationCurve _curve;
        int _lvMax = 200;
        int _expMax = 200000;

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
            if (TEST == false) return;

            int exceed = _exp - _expNeed;
            Time.timeScale = 0;

            if (_lv / 5 == 0)
            {
                foreach(PlayerSkill k in SkillManager.I._skList)
                {
                    if (k._isProjectile == true)
                        k._dmg += 3;
                    else
                        k._dmg += 1;
                }
                UIManager.I.LevelUP();
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
