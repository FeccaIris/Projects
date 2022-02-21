using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{
    [System.Serializable]
    public class Skill
    {
        public bool _isProj;
        public int _index;

        [SerializeField] float _cool = 0.5f;
        [SerializeField] float _reach = 15.0f;
        [SerializeField] int _ea = 1;

        [SerializeField] float _speed = 2000.0f;
        [SerializeField] float _size = 1.0f;
        [SerializeField] float _maintain = 2.0f;
        [SerializeField] int _pierce = 1;
        [SerializeField] int _dmg = 1;

        #region Property
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
        public int Damage
        {
            get { return _dmg; }
            set { _dmg = value; }
        }
        #endregion

        public Skill(bool isProj = true)
        {
            _isProj = isProj;
            _index = SkillManager.I._skList.Count + 1;
        }
    }

    public class SkillManager : MonoBehaviour
    {
        public static SkillManager I;

        public List<Skill> _skList;

        Player _player;

        float _distance;

        private void Awake()
        {
            I = this;
        }
        private void Start()
        {
            _player = Player.I;
            _skList = new List<Skill>();

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
            Skill n = new Skill(isProj);
            _skList.Add(n);
            StartCoroutine(Activate(n));
        }

        IEnumerator Activate(Skill skill)
        {
            GameObject prefab = null;

            if (skill._isProj == true)
            {
                prefab = Resources.Load("SV_Projectile") as GameObject;
            }
            else
            {
                prefab = Resources.Load("SV_Area") as GameObject;
            }

            while (true)
            {
                if (_player._target != null)
                {
                    if (_distance <= skill.Reach)
                    {
                        Vector3 dir = (_player._target.position - transform.position).normalized;

                        for (int i = 0; i < skill.EA; i++)
                        {
                            GameObject go = Instantiate(prefab);
                            go.transform.position = _player._firePos.position;

                            float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                            Quaternion q = Quaternion.AngleAxis(z, Vector3.forward);
                            go.transform.rotation = Quaternion.Lerp(transform.rotation, q, 1.0f);   // Àû ¹æÇâ

                            Projectile p = go.GetComponent<Projectile>();
                            p.Activate(dir, size: skill.Size, pierce: skill.Pierce, maintain: skill.Maintain, speed: skill.Speed, dmg: skill.Damage);
                            yield return new WaitForSeconds(0.2f / skill.EA);
                        }

                        yield return new WaitForSeconds(skill.Cool * Time.timeScale);
                    }
                    else
                    {
                        yield return null;
                    }
                }
                else
                {
                    yield return null;
                }
            }
        }
    }
}
