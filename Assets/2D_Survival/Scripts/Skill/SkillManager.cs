using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class SkillManager : MonoBehaviour
    {
        Player _player;

        IEnumerator _first;
        IEnumerator _second;
        IEnumerator _third;

        bool _isProjectile = true;
        float _distance;
        
        float _cool = 0.5f;
        float _reach = 15.0f;
        int _ea = 1;

        float _speed = 2000.0f;
        float _size = 1.0f;
        float _maintain = 2.0f;
        int _pierce = 1;

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
        #endregion

        private void Start()
        {
            _player = Player.I;

            IEnumerator _first = NewSkill(true);
            StartCoroutine(_first);
        }
        private void FixedUpdate()
        {
            if (_player._target != null)
            {
                _distance = Vector3.Distance(transform.position, _player._target.position);
            }   
        }

        IEnumerator NewSkill(bool isProjectile)
        {
            GameObject prefab = null;

            if (isProjectile)
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
                    if (_distance <= Reach)
                    {
                        Vector3 dir = (_player._target.position - transform.position).normalized;

                        for (int i = 0; i < EA; i++)
                        {
                            GameObject go = Instantiate(prefab);
                            go.transform.position = _player._firePos.position;
                            //go.transform.rotation = _player._fireRot.transform.rotation;
                            /*
                            if (_player._target != null)
                                dir = (_player._target.position - transform.position).normalized;
                            */
                            float z = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                            Quaternion q = Quaternion.AngleAxis(z, Vector3.forward);
                            go.transform.rotation = Quaternion.Lerp(transform.rotation, q, 1.0f);   // Àû ¹æÇâ

                            Projectile p = go.GetComponent<Projectile>();
                            p.Activate(dir, size: Size, pierce: Pierce, maintain: Maintain, speed: Speed);
                            yield return new WaitForSeconds(0.2f / EA);
                        }

                        yield return new WaitForSeconds(Cool * Time.timeScale);
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
