using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager I;
    public GameObject _player;
    public Sector _sector;

    float _spawnTime = 10.0f;

    void Awake()
    {
        I = this;
    }

    void Start()
    {
        _sector = Sector._self;
        //StartCoroutine(Spawner());
    }

    void Update()
    {

    }

    IEnumerator Spawner()
    {
        while (true)
        {
            Spawn();

            yield return new WaitForSeconds(_spawnTime);
        }
    }

    void Spawn()
    {
        foreach (Sector_Area sa in _sector._sectorArr)
        {
            if (sa.name.Contains("5"))
            {
                continue;
            }
            GameObject pref = Resources.Load("Spawner") as GameObject;
            GameObject go = Instantiate(pref);
            go.transform.position = sa.transform.position;
        }
    }
}
