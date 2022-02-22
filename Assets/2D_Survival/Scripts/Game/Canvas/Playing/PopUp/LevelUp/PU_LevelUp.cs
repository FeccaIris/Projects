using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{
    public class PU_LevelUp : MonoBehaviour
    {
        public delegate void CallBack();
        public CallBack _cb;

        public List<PlayerSkill> _skList;

        public GameObject _idTab;
        public GameObject _rfTab;

        public List<Button_SID> _idButtonList;
        public List<Button_SRF> _rfButtonList;

        public Button _costUp;

        public void Init()
        {
            _idTab = transform.Find("IndexTab").gameObject;
            _rfTab = transform.Find("ReinforceTab").gameObject;
            TurnOnIndex(true);

            _idButtonList = new List<Button_SID>(_idTab.transform.Find("IDB").GetComponentsInChildren<Button_SID>(true));
            _rfButtonList = new List<Button_SRF>(_rfTab.GetComponentsInChildren<Button_SRF>(true));
            _costUp = _idTab.transform.Find("CostUp").GetComponent<Button>();

            _costUp.onClick.AddListener(delegate()
            {
                OnEnd();
                SkillManager.I.AcquireNew(true);
            });

            foreach(Button_SID b in _idButtonList)
            {
                b.Init(this);
            }
            foreach (Button_SRF b in _rfButtonList)
            {
                b.Init(this);
            }

            Show(false);
        }
        public void Show(bool show, CallBack cb = null)
        {
            gameObject.SetActive(show);

            _cb = cb;
        }
        public void TurnOnIndex(bool b)
        {
            _idTab.SetActive(b);
            _rfTab.SetActive(!b);
        }
        public void OnEnd()
        {
            if (_cb != null)
                _cb();

            TurnOnIndex(true);
            Show(false);
        }
    }
}
