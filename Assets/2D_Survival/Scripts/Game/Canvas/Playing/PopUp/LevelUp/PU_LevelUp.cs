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

    [System.Serializable]
    public class Projectile_Table
    {
        public Skill_Projectile _ps;

        public int _ea;
        public float _speed;

        public void SetPS(Skill_Projectile ps)
        {
            _ps = ps;
        }

        public void Reinforce(ProjectileCategory cat)
        {
            switch (cat)
            {
                case ProjectileCategory.DAMAGE:
                    {
                        _ps.Damage += 1;
                        break;
                    }
                case ProjectileCategory.COOL:
                    {
                        _ps.Cool *= 0.9f;
                        break;
                    }
                case ProjectileCategory.REACH:
                    {
                        _ps.Reach *= 1.1f;
                        break;
                    }
                case ProjectileCategory.EA:
                    {
                        _ps.EA += 1;
                        break;
                    }
                case ProjectileCategory.SPEED:
                    {
                        _ps.Speed *= 1.1f;
                        break;
                    }
                case ProjectileCategory.SIZE:
                    {
                        _ps.Size *= 1.05f;
                        break;
                    }
                case ProjectileCategory.MAINTAIN:
                    {
                        _ps.Maintain *= 1.1f;
                        break;
                    }
                case ProjectileCategory.PIERCE:
                    {
                        _ps.Pierce += 1;
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

        public Projectile_Table _pjtlTable;

        public GameObject _idTab;
        public GameObject _rfTab;

        public List<Button_SID> _idButtonList;
        public List<Button_SRF> _rfButtonList;

        public Button _costUp;

        public void Init()
        {
            _pjtlTable = new Projectile_Table();

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

        public void ReadyRF(Skill_Projectile ps)
        {
            if (ps == null)
                return;

            _pjtlTable.SetPS(ps);

            ProjectileCategory a, b, c, d;

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
