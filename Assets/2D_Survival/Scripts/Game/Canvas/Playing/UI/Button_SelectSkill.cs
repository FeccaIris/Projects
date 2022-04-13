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

            onClick.AddListener(delegate ()
            {
                Debug.Log("delegate");
                Sequence1();
            });
        }

        /// <summary>
        /// 스킬 유형 결정
        /// </summary>
        public void Sequence1()
        {
            Debug.Log("1");
            /// 투사체 여부
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

            /// 추적, 무작위, 고정
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

            Sequence2(_isProj, _prop);
        }
        /// <summary>
        /// 유형별 능력치 설정
        /// </summary>
        public void Sequence2(bool proj, Property prop)
        {
            Debug.Log("2");
            if (proj == true)
            {
                switch (prop)
                {
                    case Property.Target:
                        {
                            Debug.Log("?");
                            SkillManager.I.AcquireNew();
                            break;
                        }
                    case Property.Random:
                        {
                            Debug.Log("?");
                            SkillManager.I.AcquireNew(rd: true);
                            break;
                        }
                    case Property.Static:
                        {
                            Debug.Log("?");
                            SkillManager.I.AcquireNew(st: true);
                            break;
                        }
                }
            }
            else
            {
                switch (prop)
                {
                    case Property.Target:
                        {
                            Debug.Log("?");
                            SkillManager.I.AcquireNew();
                            break;
                        }
                    case Property.Random:
                        {
                            Debug.Log("?");
                            SkillManager.I.AcquireNew(rd: true);
                            break;
                        }
                    case Property.Static:
                        {
                            Debug.Log("?");
                            SkillManager.I.AcquireNew(st: true);
                            break;
                        }
                }
            }

            Sequence3();
        }
        /// <summary>
        /// 속도, 관통, 유지, 개수, 크기, 데미지, 간격 설정
        /// </summary>
        public void Sequence3()
        {

        }
    }
}
