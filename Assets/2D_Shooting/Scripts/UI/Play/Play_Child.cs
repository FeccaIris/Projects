using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ss
{
    public class Play_Child : MonoBehaviour
    {
        PlayUI _owner;

        public virtual void Init(PlayUI owner)
        {
            _owner = owner;
            _owner._childList.Add(this);

            gameObject.SetActive(false);
        }
    }
}
