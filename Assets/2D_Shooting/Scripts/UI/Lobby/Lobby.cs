using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ss
{

    public class Lobby : MonoBehaviour
    {
        public Menu _menu;
        public Survival _survival;
        public Infinite _infinite;

        public void Init()
        {
            _menu = transform.Find("Menu").GetComponent<Menu>();
            _survival = transform.Find("Survival").GetComponent<Survival>();
            _infinite = transform.Find("Infinite").GetComponent<Infinite>();

            _menu.Init();
            _survival.Init();
            _infinite.Init();
        }
    }
}
