using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject _player;

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.0f);

            GameObject pref =  Resources.Load("Enemy1") as GameObject;
            GameObject go = Instantiate(pref);

            float start = 0f;
            float end = 10f;
            float x = Random.Range(start, end);
            float y = Random.Range(start, end);

            go.transform.position = new Vector3(x, y, 0);
        }

    }


    void Start()
    {
        StartCoroutine("SpawnEnemy");
    }

    void Update()
    {

    }
}
