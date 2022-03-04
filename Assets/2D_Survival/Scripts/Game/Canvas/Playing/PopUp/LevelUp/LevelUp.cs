using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{
    public class LevelUp : MonoBehaviour
    {
        public IndexTab _indexT;
        public ReinforceTab _reinforceT;

        public List<LevelUpPU> _puList;

        public void Init()
        {
            _indexT = transform.Find("IndexTab").GetComponent<IndexTab>();
            _reinforceT = transform.Find("ReinforceTab").GetComponent<ReinforceTab>();

            _indexT.Init(this);
            _reinforceT.Init(this);

            gameObject.SetActive(false);
        }

        public void LevelUP()
        {
            _indexT.gameObject.SetActive(true);
            gameObject.SetActive(true);

            _indexT.SetIndexButton();

            _indexT.gameObject.SetActive(true);
        }

        public void ReadyForReinforce(PlayerSkill ps)   // 인덱스 버튼 입력 = 스킬 선택
        {
            _reinforceT.SetReinforceButton(ps);
            Debug.Log(ps._index);
        }


        public void TurnOnOff(LevelUpPU on, LevelUpPU off)
        {
            off.gameObject.SetActive(false);
            on.gameObject.SetActive(true);
        }

        public void CloseAll()
        {
            foreach(LevelUpPU pu in _puList)
            {
                if(pu.gameObject.activeSelf == true)
                    pu.gameObject.SetActive(false);
            }

            gameObject.SetActive(false);

            Time.timeScale = 1;
            LevelManager.I.CheckLevelUp();
        }
    }
}
