using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject _player;

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(30.0f);
        GameObject pref =  Resources.Load("Enemy1") as GameObject;
        GameObject go = Instantiate(pref);
        go.transform.position = _player.transform.position * 2;
    }


    void Start()
    {
        StartCoroutine("SpawnEnemy");
    }

    void Update()
    {

    }
}
