using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SV
{
    public enum SceneNum
    {
        LOAD = 0,
        LOBBY = 1,
        PLAY = 2,
    }

    public class LoadingTest : MonoBehaviour
    {
        Text _progress;
        Image _bar;

        void Start()
        {
            _progress = transform.Find("Progress").GetComponent<Text>();
            _bar = transform.Find("Bar").GetComponent<Image>();
            _bar.fillAmount = 0;

            StartCoroutine(LoadScene());
        }

        IEnumerator LoadScene()
        {
            yield return null;

            AsyncOperation op = SceneManager.LoadSceneAsync((int)SceneNum.PLAY);

            op.allowSceneActivation = false;

            while (!op.allowSceneActivation)
            {
                _progress.text = $"Loading.. {op.progress * 100}%";
                _bar.fillAmount = Mathf.Lerp(_bar.fillAmount, op.progress + 0.1f, Time.fixedDeltaTime);

                if (op.progress >= 0.9f)
                {
                    _progress.text = "Press the space bar to Countinue";

                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        op.allowSceneActivation = true;
                    }
                }

                yield return null;
            }
        }
    }
}
