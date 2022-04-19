using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SV
{
    public enum Property
    {
        Target,
        Random,
        Static,
    }
    public class Button_SelectSkill : Button
    {
        public int _index;
        public bool _isProj;
        public Property _prop;

        public Image _square;
        public Image _circle;
        public Image _diamond;

        public Text _name;
        public Text _t1;
        public Text _t2;

        public void Init()
        {
            _square = transform.Find("Square").GetComponent<Image>();
            _circle = transform.Find("Circle").GetComponent<Image>();
            _diamond = transform.Find("Diamond").GetComponent<Image>();
            _name = transform.Find("Name").GetComponent<Text>();
            _t1 = transform.Find("Text1").GetComponent<Text>();
            _t2 = transform.Find("Text2").GetComponent<Text>();

            _name.text = "";

            FixSkillType();

            onClick.AddListener(delegate ()
            {
                SetSkill(_isProj, _prop);
            });
        }

        public void ChangeImage(int id)
        {
            switch (id)
            {
                case 0:
                    {
                        ShowImage(sq: true);
                        break;
                    }
                case 1:
                    {
                        ShowImage(d: true);
                        break;
                    }
                case 2:
                    {
                        ShowImage(c: true);
                        break;
                    }
            }
        }
        void ShowImage(bool sq = false, bool d = false, bool c = false)
        {
            _square.gameObject.SetActive(sq);
            _diamond.gameObject.SetActive(d);
            _circle.gameObject.SetActive(c);
        }

        /// <summary>
        /// ��ų ���� ����
        /// </summary>
        public void FixSkillType()
        {
            /// ����ü ����
            int p = Random.Range(0, 2);
            switch (p)
            {
                case 0:
                    {
                        _isProj = true;
                        break;
                    }
                case 1:
                    {
                        _isProj = false;
                        break;
                    }
                default: break;
            }

            /// ����, ������, ����
            int r = Random.Range(0, 3);
            switch (r)
            {
                case 0:
                    {
                        _prop = Property.Target;
                        break;
                    }
                case 1:
                    {
                        _prop = Property.Random;
                        break;
                    }
                case 2:
                    {
                        _prop = Property.Static;
                        break;
                    }
                default: break;
            }

            if(_isProj! == true)
            {
                _t1.text = "����ü";
            }
            else
            {
                _t1.text = "������";
            }

            switch (_prop)
            {
                case Property.Target:
                    {
                        _t2.text = "����";
                        break;
                    }
                case Property.Random:
                    {
                        _t2.text = "������";
                        break;
                    }
                case Property.Static:
                    {
                        _t2.text = "����";
                        break;
                    }
            }
        }

        /// <summary>
        /// ��ų ����
        /// </summary>
        public void SetSkill(bool proj, Property prop)
        {
            PlayerSkill ps = null;

            switch (prop)
            {
                case Property.Target:
                    {
                        ps = SkillManager.I.AcquireNew(pj: proj);

                        if(ps._isProjectile == true)
                        {
                            ps.SetSkill(dmg: 3, spd: 45, cool: 0.33f, interval: 0.2f, size: 1.2f, mt:3, rch: 30);
                        }
                        else
                        {
                            ps.SetSkill(dmg: 10, size: 10, cool: 1, interval: 2f,  mt: 0.3f, rch: 26);
                        }
                        break;
                    }
                case Property.Random:
                    {
                        ps = SkillManager.I.AcquireNew(pj: proj, rd: true);

                        if (ps._isProjectile == true)
                        {
                            ps.SetSkill(dmg: 8, cool: 0.7f, size: 5, ea: 10, spd: 50, pierce: 500, mt: 3, interval: 0.2f);
                        }
                        else
                        {
                            ps.SetSkill(dmg: 1, size: 30, cool: 1.5f, interval: 0.1f);
                        }
                        break;
                    }
                case Property.Static:
                    {
                        ps = SkillManager.I.AcquireNew(pj: proj, st: true);

                        if (ps._isProjectile == true)
                        {
                            ps.SetSkill(dmg: 3, spd: 45, ea: 3, size: 10, pierce: 3, cool: 0.7f, interval: 0.5f, mt: 3);
                        }
                        else
                        {
                            ps.SetSkill(size: 15, cool: 1, interval: 0.2f, mt: 0.6f);
                        }
                        break;
                    }
            }
        }
    }
}
