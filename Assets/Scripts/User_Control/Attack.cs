using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public static Attack I;

    public Vector2 _dir;

    private void Awake()
    {
        I = this;
    }
    private void Start()
    {
        StartCoroutine(Shoot());
    }
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Player._inst._atk.SetActive(true);
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Beam();
                yield return new WaitForSeconds(0.02f);
                Beam();
                yield return new WaitForSeconds(0.02f);
                Beam();
                yield return new WaitForSeconds(1.0f);
            }

            yield return new WaitForSeconds(0.1f);
        }
    }

    public void Beam()
    {
        GameObject go = Instantiate(Player._inst._beam);
        go.SetActive(true);
        go.transform.position = Player._inst._beam.transform.position;
        go.transform.rotation = Player._inst._beam.transform.rotation;
        beam b = go.GetComponent<beam>();
        b.Delete();
    }

    IEnumerator Shoot0()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _dir = Player._inst._dir.normalized;
                GameObject prefab = Resources.Load("Bullet") as GameObject;
                GameObject go = Instantiate(prefab);
                go.transform.position = Player._inst._firePos.position;
                go.transform.rotation = transform.rotation;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
}
