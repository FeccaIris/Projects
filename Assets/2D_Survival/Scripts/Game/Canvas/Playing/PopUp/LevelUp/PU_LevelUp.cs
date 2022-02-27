using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{
    public enum ProjectileCategory
    {
        INVALID = 0,

        DAMAGE,
        COOL,
        REACH,
        EA,
        SIZE,
        SPEED,
        MAINTAIN,
        PIERCE,

        END
    }
    public enum AreaCategory
    {
        INVALID = 0,

        DAMAGE,
        COOL,
        REACH,
        EA,
        SIZE,
        MAINTAIN,

        END
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

        public void Reinforce(ProjectileCategory cat)
        {
            switch (cat)
            {
                case ProjectileCategory.DAMAGE:
                    {
                        _pj.Damage += 1;
                        break;
                    }
                case ProjectileCategory.COOL:
                    {
                        _pj.Cool *= 0.9f;
                        break;
                    }
                case ProjectileCategory.REACH:
                    {
                        _pj.Reach *= 1.1f;
                        break;
                    }
                case ProjectileCategory.EA:
                    {
                        _pj.EA += 1;
                        break;
                    }
                case ProjectileCategory.SPEED:
                    {
                        _pj.Speed *= 1.1f;
                        break;
                    }
                case ProjectileCategory.SIZE:
                    {
                        _pj.Size *= 1.05f;
                        break;
                    }
                case ProjectileCategory.MAINTAIN:
                    {
                        _pj.Maintain *= 1.1f;
                        break;
                    }
                case ProjectileCategory.PIERCE:
                    {
                        _pj.Pierce += 1;
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

            ProjectileCategory a, b, c, d;
            // 랜덤 설정 시작
            a = (ProjectileCategory)Random.Range((int)ProjectileCategory.DAMAGE, (int)ProjectileCategory.END);

            List<ProjectileCategory> list = new List<ProjectileCategory> { a };

            while (true)
            {
                b = (ProjectileCategory)Random.Range((int)ProjectileCategory.DAMAGE, (int)ProjectileCategory.END);
                if (b != a)
                {
                    list.Add(b);
                    break;
                }
            }
            while (true)
            {
                c = (ProjectileCategory)Random.Range((int)ProjectileCategory.DAMAGE, (int)ProjectileCategory.END);
                if (c != a && c != b)
                {
                    list.Add(c);
                    break;
                }
            }
            while (true)
            {
                d = (ProjectileCategory)Random.Range((int)ProjectileCategory.DAMAGE, (int)ProjectileCategory.END);
                if (d != a && d != b && d!= c)
                {
                    list.Add(d);
                    break;
                }
            }

            foreach (Button_SRF bt in _rfButtonList)
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
