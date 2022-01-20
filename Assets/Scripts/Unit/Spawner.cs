using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Spawner : MapObject
{
    float _spawnTime = 1.0f;

    protected override void Start()
    {
        base.Start();

        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnTime);

            GameObject pref = Resources.Load("Suicider") as GameObject;
            GameObject go = Instantiate(pref);
            go.transform.position = transform.position;
        }
    }
}
