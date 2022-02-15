using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager I;

        public List<Enemy> _enemies;

        bool _playing = true;
        float _spawnCool = 3.0f;
        float _gameTime = 0.0f;

        private void Awake()
        {
            I = this;
        }
        private void Start()
        {
            StartCoroutine(SpawnEnemy());
        }
        private void FixedUpdate()
        {
            _gameTime += Time.fixedDeltaTime * Time.timeScale;
        }

        IEnumerator SpawnEnemy()
        {
            while (_playing)
            {
                yield return new WaitForSeconds(_spawnCool * Time.timeScale);

                if (Player.I != null)
                {
                    GameObject prefab = null;

                    if (_gameTime <= 300.0f)
                    {
                        prefab = Resources.Load("Walker") as GameObject;
                    }
                    else if (_gameTime <= 600.0f)
                    {

                    }
                    else
                    {

                    }
                    GameObject go = Instantiate(prefab);
                    Enemy e = go.GetComponent<Enemy>();
                    _enemies.Add(e);
                    Vector2 pos = Player.I.transform.position;
                    Vector2 random = Random.insideUnitCircle;
                    random = random.normalized;

                    go.transform.position = pos + random * new Vector2(35, 35);
                }
            }
        }
    }
}
