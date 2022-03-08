using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{
    public class LevelUp : MonoBehaviour
    {
        public FirstTab _fistTab;
        public SecondTab _secondTab;
        public ThirdTab _thirdTab;
        public NewSkillTab _newTab;

        public List<LevelUpPU> _puList;

        public void Init()
        {
            _fistTab = transform.Find("FirstTab").GetComponent<FirstTab>();
            _secondTab = transform.Find("SecondTab").GetComponent<SecondTab>();
            _thirdTab = transform.Find("ThirdTab").GetComponent<ThirdTab>();
            _newTab = transform.Find("NewSkillTab").GetComponent<NewSkillTab>();

            _fistTab.Init(this);
            _secondTab.Init(this);
            _thirdTab.Init(this);
            _newTab.Init(this);

            gameObject.SetActive(false);
        }

        public void LevelUP()
        {
            OffAll();
            _fistTab.gameObject.SetActive(true);
            gameObject.SetActive(true);
        }

        public void AcquireNew(int id)
        {
            OffAll();
            _newTab.Show(id);
            gameObject.SetActive(true);
        }

        public void SetIndex(List<PlayerSkill> list)
        {
            _secondTab.SetButtons(list);
        }

        public void ReadyForRF(PlayerSkill ps)
        {
            _thirdTab.ReadyForRF(ps);
        }

        public void OffAll()
        {
            foreach (LevelUpPU pu in _puList)
            {
                if (pu.gameObject.activeSelf == true)
                    pu.gameObject.SetActive(false);
            }
        }
        public void OnOff(LevelUpPU on, LevelUpPU off)
        {
            off.gameObject.SetActive(false);
            on.gameObject.SetActive(true);
        }

        public void CloseAll()
        {
            OffAll();
            gameObject.SetActive(false);

            Time.timeScale = 1;
            LevelManager.I.CheckLevelUp();
        }
    }
}
