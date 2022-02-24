using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{
    public enum RFTable
    {
        INVALID = 0,

        EA,
        SPEED,

        DMG,



        END
    }

    [System.Serializable]
    public class Skill_Reinforce_Table
    {
        public PlayerSkill _ps;

        public bool _increase = true;

        public int _ea;
        public float _speed;

        public void SetPS(PlayerSkill ps)
        {
            _ps = ps;
        }

        public void Reinforce(RFTable cat)
        {
            switch (cat)
            {
                case RFTable.EA:
                    {
                        _ps.EA += 1;
                        break;
                    }
                case RFTable.SPEED:
                    {
                        _ps.Speed += 500.0f;
                        break;
                    }
                case RFTable.DMG:
                    {
                        _ps.Damage += 1;
                        break;
                    }
                default:
                    break;
            }
        }
    }

    public class PU_LevelUp : MonoBehaviour
    {
        public delegate void CallBack();
        public CallBack _cb;

        public Skill_Reinforce_Table _rfTable;

        public GameObject _idTab;
        public GameObject _rfTab;

        public List<Button_SID> _idButtonList;
        public List<Button_SRF> _rfButtonList;

        public Button _costUp;

        public void Init()
        {
            _rfTable = new Skill_Reinforce_Table();

            _idTab = transform.Find("IndexTab").gameObject;
            _rfTab = transform.Find("ReinforceTab").gameObject;
            TurnOnIndex(true);

            _idButtonList = new List<Button_SID>(_idTab.transform.Find("IDB").GetComponentsInChildren<Button_SID>(true));
            _rfButtonList = new List<Button_SRF>(_rfTab.GetComponentsInChildren<Button_SRF>(true));
            _costUp = _idTab.transform.Find("CostUp").GetComponent<Button>();

            _costUp.onClick.AddListener(delegate()
            {
                SkillManager.I.AcquireNew(true);
                OnEnd();
            });

            foreach(Button_SID b in _idButtonList)
            {
                b.Init(this);
            }
            foreach (Button_SRF b in _rfButtonList)
            {
                b.Init(this);
            }

            Show(false);
        }

        public void ReadyRF(PlayerSkill ps)
        {
            if (ps == null)
                return;

            _rfTable.SetPS(ps);

            int start = ps._isProj ? (int)RFTable.EA : (int)RFTable.DMG;    // 영역형 구현시 시작점이 아닌 종료점으로 한정할 것

            RFTable a, b, c, d;

            a = (RFTable)Random.Range(start, (int)RFTable.END);
            d = (RFTable)Random.Range(start, (int)RFTable.END);             // 중복 허용

            List<RFTable> list = new List<RFTable> { a, d };

            while (true)
            {
                b = (RFTable)Random.Range(start, (int)RFTable.END);
                if (b != a)
                {
                    list.Add(b);
                    break;
                }
            }
            while (true)
            {
                c = (RFTable)Random.Range(start, (int)RFTable.END);
                if (c != a && c != b)
                {
                    list.Add(c);
                    break;
                }
            }

            foreach(Button_SRF bt in _rfButtonList)
            {
                bt.SetCategory(list[_rfButtonList.IndexOf(bt)]);
            }
        }

        public void Show(bool show, CallBack cb = null)
        {
            gameObject.SetActive(show);

            _cb = cb;
        }       // 콜백 저장
        public void TurnOnIndex(bool b)
        {
            _idTab.SetActive(b);
            _rfTab.SetActive(!b);
        }
        public void OnEnd()
        {
            if (_cb != null)
                _cb();

            TurnOnIndex(true);
            Show(false);

            LevelManager.I.CheckLevelUp();
        }                                   // 콜백 실행
    }
}
