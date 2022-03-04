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
        public Option _option;

        public SelectUnit _selectUnit;

        public List<Lobby_UI> _uiList = new List<Lobby_UI>();

        public void Init()
        {
            _menu = transform.Find("Menu").GetComponent<Menu>();
            _survival = transform.Find("Survival").GetComponent<Survival>();
            _infinite = transform.Find("Infinite").GetComponent<Infinite>();
            _option = transform.Find("Option").GetComponent<Option>();
            _selectUnit = transform.Find("SelectUnit").GetComponent<SelectUnit>();

            _menu.Init();
            _survival.Init(this);
            _infinite.Init(this);
            _option.Init(this);
            _selectUnit.Init(this);
        }

        public void CloseAll(bool menuOff = false)
        {
            foreach(Lobby_UI ui in _uiList)
            {
                if (ui.gameObject.activeSelf == false)
                    continue;

                ui.gameObject.SetActive(false);
            }

            if (menuOff == true)
                _menu.gameObject.SetActive(false);
            else
            {
                _menu.gameObject.SetActive(true);
            }
        }

        public void GameStart()
        {
            gameObject.SetActive(false);
        }
    }
}
