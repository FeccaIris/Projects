using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{

    public class LevelUpPU : MonoBehaviour
    {
        public LevelUp _owner;

        public virtual void Init(LevelUp owner)
        {
            _owner = owner;
            _owner._puList.Add(this);

            gameObject.SetActive(false);
        }
    }
}
