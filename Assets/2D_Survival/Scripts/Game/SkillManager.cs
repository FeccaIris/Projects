using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    [System.Serializable]
    public class PlayerSkill
    {
        public Player _player;

        public int _index;
        public int _level = 1;

        public int _dmg = 1;
        public float _size = 1.0f;

        #region Boolean
        public bool _isProjectile;
        
        public bool _isRandom;
        public bool _hasTarget;         // 사거리, 시작점, 방향 결정

        public bool _isMultiple;

        public bool _doesMultihit;

        public bool _hasCool;
        public bool _doesMove;
        public bool _doesStay;
        #endregion

        #region Property
        public Vector3 _startPos;
        public Vector3 _targerPos;

        public float _cool = 2.0f;
        public float _speed = 100.0f;
        public int _pierce = 1;
        public float _reach = 20.0f;
        public float _maintain = 2.0f;
        public int _ea = 1;
        public float _interval = 1.0f;
        #endregion

        public void SkillReinforce(Category cat)
        {
            _level++;
            UIManager.I.UpdateIconLevel(_index, _level);
            switch (cat)
            {
                case Category.DAMAGE:
                    {
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
                        _maintain *= 1.2f;
                        break;
                    }
                case Category.PIERCE:
                    {
                        _pierce += 1;
                        break;
                    }
                case Category.SPEED:
                    {
                        _speed *= 1.2f;
                        break;
                    }
                case Category.REACH:
                    {
                        _reach *= 1.5f;
                        break;
                    }
                default:
                    break;
            }
        }

        public PlayerSkill(bool hasC, bool pj, bool mv, bool hasT, bool stay, bool mt, bool mtH, bool rdP)
        {
            _player = Player.I;

            _index = SkillManager.I._skList.Count;

            _hasCool = hasC;
            _isProjectile = pj;
            _doesMove = mv;
            _hasTarget = hasT;
            _doesStay = stay;
            _isMultiple = mt;
            _doesMultihit = mtH;
            _isRandom = rdP;
            if (Player.I != null)
                _startPos = Player.I.transform.position;
        }
    }

    public class SkillManager : MonoBehaviour
    {
        public static SkillManager I;

        public const float TimeCor = 100_000.0f;

        Player _player;

        public List<PlayerSkill> _skList;
        int _skillLimits = 12;

        private void Awake()
        {
            I = this;
        }
        public void Init()
        {
            _player = Player.I;
            _skList = new List<PlayerSkill>();

            //AcquireNew();
            // 기본 투사체
            //ActivateSkill(_skList[0], size: 1.0f, mntn: 3.0f, spd: 100.0f, interval: 0.1f);

            AcquireNew(pj: false);
            ActivateSkill(_skList[0]);
        }

        public void AcquireNew(bool hasC = true, bool pj = true, bool mv = true, bool hasT = true,
            bool stay = true, bool mt = true, bool mtH = false, bool rdP = false)
        {
            if (_skList.Count >= _skillLimits)
                return;

            PlayerSkill ps = new PlayerSkill(hasC: hasC, pj: pj, mv: mv, hasT: hasT,
                                             stay: stay, mt: mt, mtH: mtH, rdP: rdP);

            _skList.Add(ps);

            UIManager.I._lvUp.SetIndex(_skList);
        }

        public void ActivateSkill(PlayerSkill ps, int dmg = 1, float cool = 0.7f, int ea = 1, float mntn = 2, float rch = 15, float spd = 100, int pierce = 1, float interval = 1, float size = 1.0f)
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

            Activate(ps);
        }

        public void Activate(PlayerSkill ps)
        {
            if (ps._hasCool == true)
            {
                if (ps._isProjectile == true)
                    StartCoroutine(Projectile(ps));
                else
                    return;
            }
            else
            {

            }
        }
        
        IEnumerator HasCool(PlayerSkill ps, IEnumerator skill)
        {
            while(GameManager.I._playing == true)
            {
                StartCoroutine(skill);

                yield return new WaitForSeconds(ps._cool);
            }
        }

        IEnumerator Projectile(PlayerSkill ps)
        {
            // 코루틴 추가 생성 => 스킬 발동과 동시에 쿨타임 발생

            while (GameManager.I._playing == true)
            {
                yield return null;
                Vector3 rPos = Vector3.zero;

                if (ps._hasTarget == true)
                {
                    if (_player._target == null)
                        yield return new WaitUntil(() => _player._target != null);

                    yield return new WaitUntil(() => ps._reach >= _player._distance);

                    Vector3 dir = _player._target.position - transform.position;
                    ps._targerPos = dir.normalized;
                }
                else
                {
                    if (ps._index == 1)
                    {
                        ps._targerPos = _player._forward;
                    }
                    else if(ps._index == 2)
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

                        ps._targerPos = rPos;
                    }
                }

                int ea = ps._ea;

                for (int i = 0; i < ea; i++)
                {
                    ps._startPos = transform.position;
                    if (ps._index == 1)
                    {
                        ps._targerPos = _player._forward;
                    }
                    else if (ps._index == 2)
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

                        ps._targerPos = rPos;
                    }
                    GameObject go = GameManager.I.GetPoolObject(GameManager.I._skill);
                    Skill k = go.GetComponent<Skill>();
                    k.Init(ps);
                    k.Projectile();

                    yield return new WaitForSeconds(ps._interval);
                }
                yield return new WaitForSeconds(ps._cool);
            }
        }
        IEnumerator Area(PlayerSkill ps)
        {
            while(GameManager.I._playing == true)
            {
                yield return null;
            }
        }
    }
}
