using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{

    public class Button_Skill : Button
    {
        public PU_LevelUp _owner;

        public void Init(PU_LevelUp owner)
        {
            _owner = owner;

            onClick.AddListener(delegate ()
            {
                _owner.OnSelect();

                LevelManager.I.CheckLevelUp();
            });
        }
    }
}
