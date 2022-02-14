using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{

    public class Skill : MonoBehaviour
    {
        Player P;

        float _cool = 2.0f;
        float _distance;
        float _reach = 15.0f;

        private void Start()
        {
            P = Player.I;

            StartCoroutine(NewSkill());
        }
        private void FixedUpdate()
        {
            if (P._target != null)
            {
                _distance = Vector3.Distance(transform.position, P._target.position);
            }   
        }

        IEnumerator NewSkill()
        {
            GameObject prefab = Resources.Load("SV_Bullet") as GameObject;

            while (true)
            {
                if (P._target != null)
                {
                    if (_distance <= _reach)
                    {
                        GameObject go = Instantiate(prefab);
                        go.transform.position = P._firePos.position;
                        //go.transform.rotation = _firePos.rotation;
                        Projectile p = go.GetComponent<Projectile>();
                        p._target = P._target;

                        yield return new WaitForSeconds(_cool);
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
