using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{
    public enum Category
    {
        INVALID = 0,

        PIERCE_PJ_START,
        REACH,
        
        DAMAGE_AREA_START,
        SPEED,
        COOL,
        EA,
        SIZE,
        MAINTAIN,
        
        INTERVAL_PJ_END,

        AREA_END
    }

    [System.Serializable]
    public class Reinforce_Table
    {
        public Skill_Projectile _pj;
        public Skill_Area _area;

        public void SetPS(PlayerSkill ps)
        {
            if(ps is Skill_Projectile)
            {
                _pj = ps as Skill_Projectile;
            }
            else if(ps is Skill_Area)
            {
                _area = ps as Skill_Area;
            }
        }

        public void Reinforce(PlayerSkill ps, Category cat)
        {
            if (ps is Skill_Projectile)
            {
                switch (cat)
                {
                    case Category.PIERCE_PJ_START:
                        {
                            _pj.Pierce += 1;
                            break;
                        }
                    case Category.DAMAGE_AREA_START:
                        {
                            _pj.Damage += 1;
                            break;
                        }
                    case Category.COOL:
                        {
                            _pj.Cool *= 0.9f;
                            break;
                        }
                    case Category.REACH:
                        {
                            _pj.Reach *= 1.1f;
                            break;
                        }
                    case Category.EA:
                        {
                            _pj.EA += 1;
                            break;
                        }
                    case Category.SPEED:
                        {
                            _pj.Speed *= 1.1f;
                            break;
                        }
                    case Category.SIZE:
                        {
                            _pj.Size *= 1.05f;
                            break;
                        }
                    case Category.MAINTAIN:
                        {
                            _pj.Maintain *= 1.1f;
                            break;
                        }
                    default:
                        break;
                }
            }
            else if(ps is Skill_Area)
            {
                Debug.Log("?");
            }
        }
    }

    public class PU_LevelUp : MonoBehaviour
    {
        public delegate void CallBack();
        public CallBack _cb;

        public PlayerSkill _ps;

        public Reinforce_Table _rftable;

        public GameObject _idTab;
        public GameObject _rfTab;

        public List<Button_SID> _idButtonList;
        public List<Button_SRF> _rfButtonList;

        public Button _costUp;

        public void Init()
        {
            _rftable = new Reinforce_Table();

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

        public void ReadyRF(PlayerSkill ps)    // 인덱스 버튼 입력
        {
            if (ps == null)
                return;

            _rftable.SetPS(ps);

            // 랜덤 설정 시작
            Category a, b, c, d;

            int start = 0;
            int end = 0;

            if(ps is Skill_Projectile)
            {
                start = (int)Category.PIERCE_PJ_START;
                end = (int)Category.INTERVAL_PJ_END;
            }
            else if(ps is Skill_Area)
            {
                start = (int)Category.DAMAGE_AREA_START;
                end = (int)Category.AREA_END;
            }

            a = (Category)Random.Range(start, end);

            List<Category> list = new List<Category> { a };

            while (true)
            {
                b = (Category)Random.Range(start, end);
                if (b != a)
                {
                    list.Add(b);
                    break;
                }
            }
            while (true)
            {
                c = (Category)Random.Range(start, end);
                if (c != a && c != b)
                {
                    list.Add(c);
                    break;
                }
            }
            while (true)
            {
                d = (Category)Random.Range(start, end);
                if (d != a && d != b && d!= c)
                {
                    list.Add(d);
                    break;
                }
            }

            foreach (Button_SRF bt in _rfButtonList)
            {
                bt.SetCategory(list[_rfButtonList.IndexOf(bt)], ps);
            }
        }

        public void Show(bool show, CallBack cb = null)
        {
            gameObject.SetActive(show);

            _cb = cb;
        }       // 콜백 저장
        public void TurnOnIndex(bool b)     // 인덱스 버튼 초기화시 : false
        {
            _idTab.SetActive(b);
            _rfTab.SetActive(!b);
        }
        public void OnEnd()                 // 강화 버튼 초기화시
        {
            if (_cb != null)
                _cb();

            TurnOnIndex(true);
            Show(false);

            LevelManager.I.CheckLevelUp();
        }                                   // 콜백 실행
    }
}
