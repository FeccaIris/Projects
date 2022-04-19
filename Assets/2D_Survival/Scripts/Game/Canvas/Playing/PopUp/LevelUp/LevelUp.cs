using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{
    public class LevelUp : MonoBehaviour
    {
        public FirstTab _fistTab;
        public ReinforceTab _reinforceTab;
        public ThirdTab _thirdTab;
        public NewSkillTab _newTab;

        public List<LevelUpPU> _puList;

        public void Init()
        {
            _fistTab = transform.Find("FirstTab").GetComponent<FirstTab>();
            _reinforceTab = transform.Find("ReinforceTab").GetComponent<ReinforceTab>();
            _thirdTab = transform.Find("ThirdTab").GetComponent<ThirdTab>();
            _newTab = transform.Find("NewSkillTab").GetComponent<NewSkillTab>();

            _fistTab.Init(this);
            _reinforceTab.Init(this);
            _thirdTab.Init(this);
            _newTab.Init(this);

            gameObject.SetActive(false);
        }

        // 강화 준비 및 레벨업 ui 활성화
        public void LevelUP()
        {
            OffAll();
            _reinforceTab.ReadyReinforce();
            gameObject.SetActive(true);
        }

        public void AcquireNew(int id)
        {
            OffAll();
            _newTab.Show(id);
            gameObject.SetActive(true);
        }

        public void SetIndex(PlayerSkill ps)
        {
            _reinforceTab._buttons[ps._index]._ps = ps;
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
            Player.I.ImmuneFor(1.0f);

            LevelManager.I.CheckLevelUp();
        }
    }
}
