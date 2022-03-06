using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{
    public enum Category
    {
        DMG,
        SIZE,

        COOL,
        SPEED,
        PIERCE,
        REACH,
        MAINTAIN,
        EA,
        INTREVAL,

        RANDOM_START,
        RANDOM_DIR,
    }


    public class ReinforceTab : LevelUpPU
    {
        public List<Button_LevelUp> _buttons;

        public List<Category> _cats;

        public override void Init(LevelUp owner)
        {
            _cats = new List<Category> { Category.DMG, Category.SIZE };

            _buttons = new List<Button_LevelUp>(GetComponentsInChildren<Button_LevelUp>());

            foreach(Button_LevelUp b in _buttons)
            {
                b.onClick.AddListener(delegate ()
                {
                    // 스킬 강화 미구현

                    _owner.CloseAll();
                });
            }

            base.Init(owner);
        }

        public void SetReinforceButton(PlayerSkill ps)
        {
            CheckSkillProperty(ps);
            
            foreach (Button_LevelUp b in _buttons)
            {
                // 강화 버튼 설정

            }
        }

        public void CheckSkillProperty(PlayerSkill ps)
        {
            #region 
            // 스킬별 특성 확인 후 선택지 제공
            // enum 리스트 생성 후, 여부가 참일 시 추가
            // 리스트 중 랜덤 속성 선택 후 제공

            // 쿨타임   여부 => 쿨타임              / 거짓일 시 유지 무효
            // 유지     여부 => 지속시간
            // 이동     여부 => 속도
            // 투사체   여부 => 관통력              / 참일 시 다중히트 무효
            // 목표     여부 => 사거리, 무작위 옵션 / 참일 시 랜덤시작 무효
            // 랜덤시작 여부 => 무작위 옵션
            // 다중실행 여부 => 개수
            // 다중히트 여부 => 피해 간격
            #endregion

            if (ps._hasCool == true)
            {
                _cats.Add(Category.COOL);
            }
            else
            {
                ps._doesStay = false;
            }
           /**/
            if (ps._doesStay == true)
            {
                _cats.Add(Category.MAINTAIN);
            }
           /**/
            if (ps._doesMove == true)
            {
                _cats.Add(Category.SPEED);
            }
           /**/
            if (ps._isProjectile == true)
            {
                ps._doesMultihit = false;
                _cats.Add(Category.PIERCE);
            }
           /**/
            if (ps._hasTarget == true)
            {
                ps._atRandom = false;
            }
           /**/
            if (ps._atRandom == true) { }
           /**/
            if (ps._isMultiple == true) { }
           /**/
            if (ps._doesMultihit == true) { }


        }
    }
}
