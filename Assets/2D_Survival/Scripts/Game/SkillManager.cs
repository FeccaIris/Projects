using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    [System.Serializable]
    public class PlayerSkill
    {
        public bool _isProj;
        public int _index;
        public int _cost;

        [SerializeField] int _dmg = 1;
        [SerializeField] float _cool = 1.0f;
        [SerializeField] float _reach = 15.0f;
        [SerializeField] int _ea = 1;
        [SerializeField] float _speed = 100.0f;
        [SerializeField] float _size = 1.0f;
        [SerializeField] float _maintain = 2.0f;

        #region Property
        public int Damage
        {
            get { return _dmg; }
            set { _dmg = value; }
        }
        public float Cool
        {
            get { return _cool; }
            set { _cool = value; }
        }
        public float Reach
        {
            get { return _reach; }
            set { _reach = value; }
        }
        public int EA
        {
            get { return _ea; }
            set { _ea = value; }
        }
        public float Speed
        {
            get { return _speed; }
            set { _speed = value; }
        }
        public float Size
        {
            get { return _size; }
            set { _size = value; }
        }
        public float Maintain
        {
            get { return _maintain; }
            set { _maintain = value; }
        }
        #endregion

        public PlayerSkill(bool isPj)
        {
            _isProj = isPj;
        }
    }

    [System.Serializable]
    public class Skill_Area : PlayerSkill
    {
        [SerializeField] float _interval = 1.0f;

        #region Property

        public float Interval
        {
            get { return _interval; }
            set { _interval = value; }
        }
        #endregion

        public Skill_Area(bool isPj = false) : base(isPj)
        {
            _isProj = isPj;
            _index = SkillManager.I._skList.Count;

            Damage = 1;
            EA = 1;
            Cool = 5.0f;
            Speed = 0.0f;
            Size = 1.0f;
            Maintain = 2.0f;
            Interval = 0.5f;
        }
    }

    [System.Serializable]
    public class Skill_Projectile : PlayerSkill
    {

        [SerializeField] int _pierce = 1;

        #region Property

        public int Pierce
        {
            get { return _pierce; }
            set { _pierce = value; }
        }
        #endregion

        public Skill_Projectile(bool isPj = true) : base(isPj)
        {
            _isProj = isPj;
            _index = SkillManager.I._skList.Count;

            Damage = 1;
            EA = 1;
            Cool = 1.0f;
            Speed = 100.0f;
            Size = 1.0f;
            Maintain = 2.0f;
            Reach = 15.0f;
            Pierce = 1;
        }
    }

    public class SkillManager : MonoBehaviour
    {
        public static SkillManager I;

        public List<PlayerSkill> _skList;
        int _skMax = 12;

        Player _player;

        int _cost;

        float _distance;

        private void Awake()
        {
            I = this;
        }
        public void Init()
        {
            _player = Player.I;
            _skList = new List<PlayerSkill>();

            AcquireNew(false);
        }
        private void FixedUpdate()
        {
            if (_player != null)
            {
                if (_player._target != null)
                {
                    _distance = Vector3.Distance(transform.position, _player._target.position);
                }
            }
        }

        public void AcquireNew(bool isProj)
        {
            if (_skList.Count == _skMax)
                return;

            PlayerSkill newskill = null;

            if(isProj == true)
            {
                Skill_Projectile pj = new Skill_Projectile();
                newskill = pj;
            }
            else
            {
                Skill_Area area = new Skill_Area();
                newskill = area;
            }

            if (newskill != null)
            {
                _skList.Add(newskill);
                ActivateSkill(newskill);
                UIManager.I._levelUp._idButtonList[newskill._index].Set(newskill);
            }
        }

        public void ActivateSkill(PlayerSkill skill)
        {
            if (skill is Skill_Projectile)
            {
                Skill_Projectile pj = skill as Skill_Projectile;
                GameObject prefab = GameManager.I._proj;
                StartCoroutine(Projectile(pj, prefab));
            }
            else if(skill is Skill_Area)
            {
                Skill_Area area = skill as Skill_Area;
                GameObject prefab = GameManager.I._area;
                StartCoroutine(Area(area, prefab));
            }
        }

        IEnumerator Projectile(Skill_Projectile pj, GameObject pf)
        {
            Skill_Projectile skill = pj;
            GameObject prefab = pf;

            yield return new WaitUntil(() => Time.timeScale > 0);

            while (true)
            {
                Vector3 dir = new Vector3();
                if (_player._target != null)
                    dir = (_player._target.position - transform.position).normalized;

                yield return new WaitUntil(() => _distance <= skill.Reach);

                for (int i = 0; i < skill.EA; i++)
                {
                    yield return new WaitUntil(() => Time.timeScale > 0);

                    if (_player._target == null)
                        break;

                    GameObject go = GameManager.I.GetPoolObject(prefab);
                    go.transform.position = _player._firePos.position;
                    go.SetActive(true);

                    float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    Quaternion q = Quaternion.AngleAxis(z, Vector3.forward);

                    go.transform.rotation = Quaternion.Lerp(transform.rotation, q, 1.0f);   // Àû ¹æÇâ

                    Projectile p = go.GetComponent<Projectile>();
                    p.Init();
                    p.Activate(dir, size: skill.Size, pierce: skill.Pierce, maintain: skill.Maintain, speed: skill.Speed, dmg: skill.Damage);

                    yield return new WaitForSeconds(0.2f / skill.EA);
                }

                Player.I.ChangeTarget();

                yield return new WaitForSeconds(skill.Cool);
            }
        }

        IEnumerator Area(Skill_Area area, GameObject pf)
        {
            Skill_Area skill = area;
            GameObject prefab = pf;
            Vector3 pos = _player.transform.position;

            yield return new WaitUntil(() => Time.timeScale > 0);

            GameObject go = GameManager.I.GetPoolObject(prefab);
            go.transform.position = _player.transform.position;
            go.SetActive(true);
            Area a = go.GetComponent<Area>();
            a.Init();
            a.Activate(area);

            while (true)
            {
                if (area.Speed > 0)
                {
                    yield return null;
                }
                else
                {
                    yield return null;
                    pos = _player.transform.position;
                    go.transform.position = pos;
                }
            }
        }
    }
}
