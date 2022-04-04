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
        public bool _hasCool;           // 쿨타임
        
        public bool _isProjectile;      // 관통횟수, 발사개수 1개 이상일 시 공격속도
        
        public bool _isRandom;          // 무작위 목표 여부
        public bool _doesStay;          // Area :: 유지시간, 공격속도
        

        



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
            UIManager.I.UpdateIconLevel(_index, _level);






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
                        if (_doesStay != true)
                            _doesStay = true;

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
                default:
                    break;
            }
        }

        public PlayerSkill(bool hasC, bool pj, bool stay, bool rdP)
        {
            _player = Player.I;
            _index = SkillManager.I._skList.Count;

            _hasCool = hasC;
            _isProjectile = pj;

            _doesStay = stay;
            _isRandom = rdP;
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

            //AcquireNew();
            //SetAndActivate(_skList[0], size: 1.0f, mntn: 3.0f, spd: 100.0f, interval: 0.1f);
            // 기본 투사체

            AcquireNew(pj: false, stay: false);
            SetAndActivate(_skList[0], size: 20.0f, mntn: 0.5f, cool: 3.0f, interval: 0.2f);
        }

        public void AcquireNew(bool hasC = true, bool pj = true, bool stay = true, bool rdP = false)
        {
            PlayerSkill ps = new PlayerSkill(hasC: hasC, pj: pj, stay: stay, rdP: rdP);

            _skList.Add(ps);

            UIManager.I._lvUp.SetIndex(_skList);
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
            // 코루틴 추가 생성 => 스킬 발동과 동시에 쿨타임 발생

            while (GameManager.I._playing == true)
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
        }
        IEnumerator Area(PlayerSkill ps)
        {
            while(GameManager.I._playing == true)
            {
                GameObject go = GameManager.I.GetPoolObject(GameManager.I._skill);

                Skill k = go.GetComponent<Skill>();
                k.Init(ps);


                yield return new WaitForSeconds(ps._cool);
            }
        }
    }
}
