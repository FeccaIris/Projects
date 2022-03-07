using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ss
{
    public class PlayUI : MonoBehaviour
    {
        public Tutorial _tutorial;
        public Result _result;

        public List<Play_Child> _childList = new List<Play_Child>();

        public void Init()
        {
            _tutorial = transform.Find("Tutorial").GetComponent<Tutorial>();
            _result = transform.Find("Result").GetComponent<Result>();

            _tutorial.Init(this);
            _result.Init(this);
        }

        public void TutorialStart()
        {
            _tutorial.gameObject.SetActive(true);
        }
        public void GameStart()
        {

        }
    }
}
