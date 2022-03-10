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
        public int _dmg = 1;
        public float _size = 1.0f;

        #region Boolean
        public bool _isProjectile;
        
        public bool _isRandom;
        public bool _hasTarget;         // ��Ÿ�, ������, ���� ����

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
        public float _reach = 25.0f;
        public float _maintain = 2.0f;
        public int _ea = 1;
        public float _interval = 1.0f;
        #endregion

        #endregion

        public void SkillReinforce(Category cat)
        {
            switch (cat)
            {
                case Category.DAMAGE:
                    {
                        _dmg += 2;
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
                        _reach *= 1.2f;
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
        int _costLimits;

        private void Awake()
        {
            I = this;
        }
        public void Init()
        {
            _player = Player.I;
            _skList = new List<PlayerSkill>();

            AcquireNew();   // �⺻ : ����ü
        }

        public void AcquireNew(bool hasC = true, bool pj = true, bool mv = true, bool hasT = false,
            bool stay = true, bool mt = true, bool mtH = false, bool rdP = false)
        {
            if (_skList.Count >= _skillLimits)
                return;

            PlayerSkill ps = new PlayerSkill(hasC: hasC, pj: pj, mv: mv, hasT: hasT,
                                             stay: stay, mt: mt, mtH: mtH, rdP: rdP);
            _skList.Add(ps);
            SetSkill(ps, size: 2.0f, mntn: 3.0f, spd: 150.0f);

            UIManager.I._lvUp.SetIndex(_skList);
        }

        public void SetSkill(PlayerSkill ps, int dmg = 2, float cool = 0.7f, int ea = 1, float mntn = 2, float rch = 15, float spd = 100, int pierce = 1, float interval = 1, float size = 1.0f)
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

        IEnumerator Projectile(PlayerSkill ps)
        {
            // �ڷ�ƾ �߰� ���� => ��ų �ߵ��� ���ÿ� ��Ÿ�� �߻�

            while (true)
            {
                yield return new WaitForSeconds(ps._cool);

                if (ps._hasTarget == true)
                {
                    if (_player._target == null)
                        yield return new WaitUntil(() => _player._target != null);
                    else
                        yield return new WaitUntil(() => ps._reach >= _player._distance);

                    Vector3 dir = _player._target.position - transform.position;
                    ps._targerPos = dir.normalized;
                }
                else
                {
                    if (ps._index == 0)
                    {
                        ps._targerPos = _player._forward;
                    }
                    else if(ps._index == 2)
                    {

                    }
                }

                ps._startPos = transform.position;

                for (int i = 0; i < ps._ea; i++)
                {
                    GameObject go = GameManager.I.GetPoolObject(GameManager.I._skill);
                    Skill k = go.GetComponent<Skill>();
                    k.Init(ps);
                    k.Projectile();

                    yield return new WaitForSeconds(0.15f / ps._ea);
                }
            }
        }
    }
}
