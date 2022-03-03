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
        public int _cost;


        public bool _hasCool;
        public bool _doesMove;
        public bool _isProjectile;
        public bool _hasTarget;
        public bool _doesStay;
        public bool _isMultiple;
        public bool _doesMultihit;
        
        public int _dmg = 100;
        public float _size;

        public float _cool = 1.0f;
        public float _speed = 100.0f;
        public int _pierce = 2;
        public float _reach;
        public float _maintain = 2.0f;
        public int _ea;
        public float _interval;

        public PlayerSkill(bool hasC, bool pj, bool mv, bool hasT, bool stay, bool mt, bool mtH)
        {
            _player = Player.I;

            _hasCool = hasC;
            _isProjectile = pj;
            _doesMove = mv;
            _hasTarget = hasT;
            _doesStay = stay;
            _isMultiple = mt;
            _doesMultihit = mtH;
        }
    }

    public class SkillManager : MonoBehaviour
    {
        public static SkillManager I;

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

            AcquireNew(hasC: true, pj: true, hasT: true, mv: true, stay: true, mt: true, mtH: false);
        }

        public void AcquireNew(bool hasC, bool pj, bool mv, bool hasT, bool stay, bool mt, bool mtH)
        {
            if (_skList.Count >= _skillLimits)
                return;

            PlayerSkill ps = new PlayerSkill(hasC: hasC, pj: pj, mv: mv, hasT: hasT, stay: stay, mt: mt, mtH: mtH);
            _skList.Add(ps);
            Activate(ps);
        }

        public void Activate(PlayerSkill ps)
        {
            if(ps._hasCool == true)
            {
                StartCoroutine(_Skill(ps));
            }
            else
            {
                Skill(ps);
            }
        }

        IEnumerator _Skill(PlayerSkill ps)
        {
            while (true)
            {
                Skill(ps);
                yield return new WaitForSeconds(ps._cool);
            }
        }

        public void Skill(PlayerSkill ps)
        {
            GameObject go = GameManager.I.GetPoolObject(GameManager.I._skill);
            Skill k = go.GetComponent<Skill>();
            k.Init(ps);
            k.Activate();
        }

       


    }   
}
