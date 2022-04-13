using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    [System.Serializable]
    public class PlayerSkill
    {
        public Player _player;
        public Dictionary<Category, int> _categoryLevels = new Dictionary<Category, int>
        {
            {Category.DAMAGE, 1 },
            {Category.COOL, 1 },
            {Category.EA, 1 },
            {Category.INTERVAL, 1 },
            {Category.MAINTAIN, 1 },
            {Category.PIERCE, 1 },
            {Category.REACH, 1 },
            {Category.SIZE, 1 },
            {Category.SPEED, 1 },
        };

        public int _index;
        public int _level = 1;

        public int _dmg = 1;
        public float _size = 1.0f;

        #region Boolean  
        public bool _isProjectile;      // 관통횟수, 발사개수 1개 이상일 시 공격속도
        
        public bool _isRandom;          // 무작위 목표 여부
        public bool _isStatic;
        

        
        #endregion

        #region Property
        public Vector3 _targetPos;

        public float _cool = 2.0f;
        public int _ea = 1;

        public int _pierce = 1;
        public float _speed = 100.0f;
        
        public float _reach = 20.0f;
        
        public float _maintain = 2.0f;
        public float _interval = 1.0f;
        #endregion

        public void SkillReinforce(Category cat)
        {
            _level++;
            UIManager.I.UpdateIconLevel(this);
            _categoryLevels[cat]++;

            switch (cat)
            {
                case Category.DAMAGE:
                    {
                        if (_isProjectile)
                            _dmg += 2;
                        else
                            _dmg += 1;
                        break;
                    }
                case Category.COOL:
                    {
                        _cool *= 0.9f;
                        break;
                    }
                case Category.EA:
                    {
                        _ea += 1;
                        break;
                    }
                case Category.MAINTAIN:
                    {
                        _maintain *= 1.5f;
                        break;
                    }
                case Category.PIERCE:
                    {
                        _pierce += 1;
                        break;
                    }
                case Category.SPEED:
                    {
                        _speed *= 1.5f;
                        break;
                    }
                case Category.REACH:
                    {
                        _reach *= 1.5f;
                        break;
                    }
                case Category.INTERVAL:
                    {
                        _interval *= 0.9f;
                        break;
                    }
                case Category.SIZE:
                    {
                        _size *= 1.33f;
                        break;
                    }
                default:
                    break;
            }
        }

        public PlayerSkill(bool pj, bool rd, bool st)
        {
            _player = Player.I;
            _index = SkillManager.I._skList.Count;

            _isProjectile = pj;
            _isRandom = rd;
            _isStatic = st;
        }
    }

    public class SkillManager : MonoBehaviour
    {
        public static SkillManager I;

        public const float TimeCor = 100_000.0f;

        Player _player;

        public List<PlayerSkill> _skList;

        private void Awake()
        {
            I = this;
        }
        public void Init()
        {
            _player = Player.I;
            _skList = new List<PlayerSkill>();
        }
        public void GameStart()
        {
            AcquireNew();   // 기본 투사체
            SetAndActivate(_skList[0], size: 1.0f, mntn: 3.0f, spd: 100.0f, interval: 0.1f);

            AcquireNew(pj: false);
            SetAndActivate(_skList[1], size: 20.0f, mntn: 0.5f, cool: 3.0f, interval: 0.2f);

            AcquireNew(rd: true);
            SetAndActivate(_skList[2], size: 3.0f, mntn: 3.0f, spd: 50.0f, interval: 0.1f, pierce: 3);
        }
        public void AcquireNew(bool pj = true, bool rd = false, bool st = false)
        {
            PlayerSkill ps = new PlayerSkill(pj: pj, rd: rd, st: st);
            _skList.Add(ps);

            UIManager.I._lvUp._secondTab.SetButton(ps);
        }

        public void SetAndActivate(PlayerSkill ps, int dmg = 1, float cool = 0.7f, int ea = 1, float mntn = 2, float rch = 15, float spd = 100, int pierce = 1, float interval = 1, float size = 1.0f)
        {
            ps._dmg = dmg;
            ps._cool = cool;
            ps._ea = ea;
            ps._maintain = mntn;
            ps._reach = rch;
            ps._speed = spd;
            ps._pierce = pierce;
            ps._interval = interval;
            ps._size = size;

            UIManager.I._lvUp.SetIndex(ps);
            Activate(ps);
        }

        public void Activate(PlayerSkill ps)
        {
            if (ps._isProjectile)
                StartCoroutine(Projectile(ps));
            else
                StartCoroutine(Area(ps));
        }
        
        IEnumerator Projectile(PlayerSkill ps)
        {
            while (true)
            {
                if (GameManager.I._isPlaying == true)
                {
                    Vector3 rPos = Vector3.zero;

                    int ea = ps._ea;
                    for (int i = 0; i < ea; i++)
                    {
                        GameObject go = GameManager.I.GetPoolObject(GameManager.I._skill);

                        if (ps._isRandom)
                        {
                            while (true)
                            {
                                float x = Random.Range(-1, 1.1f);
                                float y = Random.Range(-1, 1.1f);
                                Vector3 pos = new Vector3(x, y, 0);
                                pos = pos.normalized;

                                if (rPos != pos)
                                {
                                    rPos = pos;
                                    break;
                                }
                                yield return null;
                            }

                            ps._targetPos = rPos;
                        }
                        else
                        {
                            if (_player._target == null)
                                yield return new WaitUntil(() => _player._target != null);
                            else
                                yield return new WaitUntil(() => ps._reach >= _player._distance);

                            if (_player._target != null)
                            {
                                Vector3 dir = _player._target.position - transform.position;
                                ps._targetPos = dir.normalized;
                            }
                        }

                        Skill k = go.GetComponent<Skill>();
                        k.Init(ps);

                        yield return new WaitForSeconds(ps._interval);
                    }

                    yield return new WaitForSeconds(ps._cool);
                }

                yield return null;
            }
        }
        IEnumerator Area(PlayerSkill ps)
        {
            while(GameManager.I._isPlaying == true)
            {
                GameObject go = GameManager.I.GetPoolObject(GameManager.I._skill);

                Skill k = go.GetComponent<Skill>();
                k.Init(ps);


                yield return new WaitForSeconds(ps._cool);
            }
        }
    }
}
