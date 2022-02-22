using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{
    public class Skill : MonoBehaviour, IPoolable
    {
        public void EndUse()
        {
            gameObject.SetActive(false);
            GameManager.I.RefillPool(gameObject);
        }
    }
}
