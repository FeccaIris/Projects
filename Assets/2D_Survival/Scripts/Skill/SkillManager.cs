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
    }

    [System.Serializable]
    public class Skill_Area : PlayerSkill
    {

    }

    [System.Serializable]
    public class Skill_Projectile : PlayerSkill
    {
        [SerializeField] int _dmg = 1;

        [SerializeField] float _cool = 1.0f;
        [SerializeField] float _reach = 15.0f;
        [SerializeField] int _ea = 1;

        [SerializeField] float _speed = 100.0f;
        [SerializeField] float _size = 1.0f;
        [SerializeField] float _maintain = 2.0f;
        [SerializeField] int _pierce = 1;

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
        public int Pierce
        {
            get { return _pierce; }
            set { _pierce = value; }
        }
        #endregion

        public Skill_Projectile(bool isProj = true)
        {
            _isProj = isProj;
            _index = SkillManager.I._skList.Count;
        }
    }

    public class SkillManager : MonoBehaviour
    {
        public static SkillManager I;

        public List<Skill_Projectile> _skList;
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
            _skList = new List<Skill_Projectile>();

            AcquireNew(true);
        }
        private void FixedUpdate()
        {
            if (_player._target != null)
            {
                _distance = Vector3.Distance(transform.position, _player._target.position);
            }   
        }

        public void AcquireNew(bool isProj)
        {
            if (_skList.Count == _skMax)
                return;

            Skill_Projectile n = new Skill_Projectile(isProj);
            _skList.Add(n);
            StartCoroutine(Activate(n));

            UIManager.I._levelUp._idButtonList[n._index].Set(n);
        }

        IEnumerator Activate(Skill_Projectile skill)
        {
            yield return new WaitUntil(() => Time.timeScale > 0);

            GameObject prefab = null;

            if (skill._isProj == true)
            {
                prefab = GameManager.I._proj;
            }
            else
            {
                
            }

            while (true)
            {
                Vector3 dir = new Vector3(); 

                for (int i = 0; i < skill.EA; i++)
                {
                    yield return new WaitUntil(() => Time.timeScale > 0);
                    yield return new WaitUntil(() => _distance <= skill.Reach);
                    if (_player._target == null)
                        break;

                    if (_player._target != null)
                        dir = (_player._target.position - transform.position).normalized;

                    GameObject go = GameManager.I.GetPoolObject(prefab);
                    go.transform.position = _player._firePos.position;
                    
                    float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                    Quaternion q = Quaternion.AngleAxis(z, Vector3.forward);

                    go.transform.rotation = Quaternion.Lerp(transform.rotation, q, 1.0f);   // Àû ¹æÇâ

                    Projectile p = go.GetComponent<Projectile>();
                    p.Init();
                    p.Activate(dir, size: skill.Size, pierce: skill.Pierce, maintain: skill.Maintain, speed: skill.Speed, dmg: skill.Damage);

                    yield return new WaitForSeconds(0.2f / skill.EA);
                }

                Player.I.ChangeTarget();

                yield return new WaitForSeconds(skill.Cool * Time.timeScale);
            }
        }
    }
}
