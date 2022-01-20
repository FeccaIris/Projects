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
        StartCoroutine(Melee());
        StartCoroutine(Shoot());
    }
    void Update()
    {

    }

    IEnumerator Melee()
    {
        while (true)
        {
            if (Input.GetMouseButton(1))
            {
                Player.I._atk.SetActive(true);
            }
            yield return null;
        }
    }

    IEnumerator Shoot()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Beam();
            }

            yield return new WaitForSeconds(0.02f);
        }
    }

    public void Beam()
    {
        GameObject go = Instantiate(Player.I._beam);
        go.SetActive(true);
        go.transform.position = Player.I._beam.transform.position;
        go.transform.rotation = Player.I._beam.transform.rotation;
        beam b = go.GetComponent<beam>();
        b.Delete();
    }

    IEnumerator Shoot0()
    {
        while (true)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                _dir = Player.I._dir.normalized;
                GameObject prefab = Resources.Load("Bullet") as GameObject;
                GameObject go = Instantiate(prefab);
                go.transform.position = Player.I._firePos.position;
                go.transform.rotation = transform.rotation;
            }

            yield return new WaitForSeconds(0.2f);
        }
    }
}
