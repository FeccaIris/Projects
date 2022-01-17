using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager _inst;
    public GameObject _player;
    public Sector _sector;
    public float _spwanTime = 3.0f;

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if(Time.time > 10.0f)
            {
                if(_spwanTime != 1.0f)
                {
                    _spwanTime = 1.0f;
                }
            }

            yield return new WaitForSeconds(_spwanTime);

            foreach (Sector_Area sa in _sector._sectorArr)
            {
                if (sa.name.Contains("5"))
                {
                    continue;
                }
                GameObject pref = Resources.Load("Enemy1") as GameObject;
                GameObject go = Instantiate(pref);
                go.transform.position = sa.transform.position;
            }
        }
    }

    void Awake()
    {
        _inst = this;
    }

    void Start()
    {
        _sector = Sector._self;
        StartCoroutine(SpawnEnemy());
    }

    void Update()
    {

    }
}
