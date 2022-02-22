using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{

    public class Button_SRF : Button
    {
        public PU_LevelUp _owner;

        public void Init(PU_LevelUp owner)
        {
            _owner = owner;

            onClick.AddListener(delegate ()
            {





                _owner.OnEnd();

                LevelManager.I.CheckLevelUp();
            });
        }
    }
}
