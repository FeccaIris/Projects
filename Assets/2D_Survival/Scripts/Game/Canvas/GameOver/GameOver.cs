using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SV
{

    public class GameOver : MonoBehaviour
    {
        public Button _restart;

        public void Init()
        {
            Button b = transform.Find("Restart").GetComponent<Button>();
            b.onClick.AddListener(delegate ()
            {
                SceneManager.LoadScene("Loading");
            });

            gameObject.SetActive(false);
        }
    }
}
