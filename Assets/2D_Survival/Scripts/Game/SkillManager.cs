using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    [System.Serializable]
    public class PlayerSkill
    {
        #region Member
        public Player _player;

        public int _index;
        public int _cost;

        #region Boolean
        public bool _hasCool;
        public bool _doesMove;
        public bool _isProjectile;
        public bool _hasTarget;         // 사거리, 시작점, 방향 결정
        public bool _doesStay;
        public bool _isMultiple;
        public bool _doesMultihit;
        public bool _atRandom;
        #endregion

        #region Attribute
        public int _dmg = 1;
        public float _size = 1.0f;
        public Vector3 _startPos;

        public float _cool = 1.0f;
        public float _speed = 100.0f;
        public int _pierce = 1;
        public float _reach = 25.0f;
        public float _maintain = 2.0f;
        public int _ea = 1;
        public float _interval = 1.0f;
        #endregion

        #endregion

        public void Reinforce()
        {
            
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
            _atRandom = rdP;
            if(Player.I != null)
                _startPos = Player.I.transform.position;
        }
    }

    public class SkillManager : MonoBehaviour
    {
        public static SkillManager I;

        public const float TimeCor = 100000.0f;

        Player _player;

        public List<PlayerSkill> _skList;
        int _skillLimits = 12;
        int _costLimits;

        private void Awake()
        {
            I = this;
        }
        public void Init()
        {
            _player = Player.I;
            _skList = new List<PlayerSkill>();

            AcquireNew();   // 기본 : 투사체
        }

        public void AcquireNew(bool hasC = true, bool pj = true, bool mv = true, bool hasT = true,
            bool stay = true, bool mt = true, bool mtH = false, bool rdP = false)
        {
            if (_skList.Count >= _skillLimits)
                return;

            PlayerSkill ps = new PlayerSkill(hasC: hasC, pj: pj, mv: mv, hasT: hasT,
                                             stay: stay, mt: mt, mtH: mtH, rdP: rdP);
            _skList.Add(ps);
            Activate(ps);
        }

        public void Activate(PlayerSkill ps)
        {
            if (ps._hasCool == true)
            {
                StartCoroutine(ActivateHasCool(ps));
            }
            else
            {

            }
        }

        IEnumerator ActivateHasCool(PlayerSkill ps)
        {
            while (true)
            {
                yield return new WaitUntil(() => _player._target != null);

                if (ps._hasTarget == true)                          // 사거리 체크
                    yield return new WaitUntil(() => ps._reach >= _player._distance);

                if (ps._atRandom == true)                           // 시작 위치 체크
                {
                    //ps._startPos = Random;                        // 무작위 시작 위치 미구현
                }
                else
                {
                    ps._startPos = _player.transform.position;
                }

                if (ps._doesMove == true)                           // 이동 체크
                {
                    Vector3 dir = new Vector3();

                    if (ps._hasTarget == true)                      // 목표 여부 체크
                    {
                        if (_player == null) break;
                        if (_player._target != null)
                            dir = _player._target.position - transform.position;
                        dir = dir.normalized;
                    }
                    else
                    {
                        // 랜덤 미구현
                    }

                    if (ps._isMultiple == true)                     // 다중 여부 체크
                    {
                        for (int i = 0; i < ps._ea; i++)
                        {
                            GameObject go = GameManager.I.GetPoolObject(GameManager.I._skill);
                            Skill k = go.GetComponent<Skill>();
                            k.Init(ps);
                            k.Activate();

                            k._rgd.AddForce(dir * Time.fixedDeltaTime * TimeCor * Time.timeScale);
                            k._rgd.velocity = Vector3.zero;

                            yield return new WaitForSeconds(0.1f / ps._cool);
                        }
                    }
                    else
                    {
                        yield return null;  // 단일 발동 미구현
                    }
                }
                else
                {
                    yield return null;      // 비이동 미구현
                }
                yield return new WaitForSeconds(ps._cool);          // 쿨타임
            }
        }
    }   
}
