using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ss
{

    public class SelectUnit : Lobby_Child
    {
        public override void Init(LobbyUI owner)
        {
            base.Init(owner);

            foreach(Button b in _buttons)
            {
                if (b.name.Contains("Unit"))
                {
                    b.onClick.AddListener(delegate ()
                    {
                        _owner.GameStart();
                    });
                }
            }
        }
    }
}
