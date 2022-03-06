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
                    // ��ų ��ȭ �̱���

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
                // ��ȭ ��ư ����

            }
        }

        public void CheckSkillProperty(PlayerSkill ps)
        {
            #region 
            // ��ų�� Ư�� Ȯ�� �� ������ ����
            // enum ����Ʈ ���� ��, ���ΰ� ���� �� �߰�
            // ����Ʈ �� ���� �Ӽ� ���� �� ����

            // ��Ÿ��   ���� => ��Ÿ��              / ������ �� ���� ��ȿ
            // ����     ���� => ���ӽð�
            // �̵�     ���� => �ӵ�
            // ����ü   ���� => �����              / ���� �� ������Ʈ ��ȿ
            // ��ǥ     ���� => ��Ÿ�, ������ �ɼ� / ���� �� �������� ��ȿ
            // �������� ���� => ������ �ɼ�
            // ���߽��� ���� => ����
            // ������Ʈ ���� => ���� ����
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
