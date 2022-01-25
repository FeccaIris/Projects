using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class Spawner : Enemy
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

    public override void Damaged(int dmg = 1)
    {
        _dealt.SetActive(true);
        Invoke("DealtOff", 0.2f);
        base.Damaged(dmg);
    }

    public void DealtOff()
    {
        _dealt.SetActive(false);
    }

    protected override void Die()
    {
        Collider2D c = GetComponent<Collider2D>();
        c.enabled = false;
        Invoke("Explosion", 0.5f);
        Invoke("Del", 0.7f);
    }

    public void Explosion()
    {
        _explo.SetActive(true);
    }

    public void Del()
    {
        Destroy(gameObject);
    }
}
