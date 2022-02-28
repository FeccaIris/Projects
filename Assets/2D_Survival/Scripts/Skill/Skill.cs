using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{
    public class Skill : MonoBehaviour, IPoolable
    {
        public const float TimeCor = 1000.0f;

        public Rigidbody2D _rgd;

        public void EndUse()
        {
            gameObject.SetActive(false);
            GameManager.I.RefillPool(gameObject);
        }

        public virtual void Init()
        {
            _rgd = GetComponent<Rigidbody2D>();
        }
    }
}
