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
        PLAY = 1,
    }

    public class LoadingTest : MonoBehaviour
    {
        Text _progress;

        void Start()
        {
            _progress = transform.Find("Progress").GetComponent<Text>();

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
