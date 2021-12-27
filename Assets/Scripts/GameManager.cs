using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager _inst;
    public GameObject _player;
    public Sector _sector;

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(3.0f);

            GameObject pref =  Resources.Load("Enemy1") as GameObject;
            GameObject go = Instantiate(pref);

            Transform trans = _sector.gameObject.transform.Find("1"); // sector area 1

            float start = 0f;
            float end = 10f;
            float x = Random.Range(start, end);
            float y = Random.Range(start, end);

            go.transform.position = new Vector3(x, y, 0);
        }
    }

    void Awake()
    {
        _inst = this;
    }

    void Start()
    {
        _sector = Sector._self;
        StartCoroutine("SpawnEnemy");
    }

    void Update()
    {

    }
}
