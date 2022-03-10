using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SV
{
    public interface IPoolable
    {
        void EndUse();
    }

    public class GameManager : MonoBehaviour
    {
        public static GameManager I;

        
        public Queue<GameObject> _poolSkill = new Queue<GameObject>();
        public Queue<GameObject> _poolWalker = new Queue<GameObject>();

        public GameObject _skill;
        public GameObject _walker;

        public List<Enemy> _enemies;

        public bool _playing = true;

        float _spawnCool = 3.0f;
        float _spawnRange = 60.0f;
        float _spawnEA = 1;
        float _enemyHpDeltaT;
        
        public float _gameTime = 0.0f;
        public float _elapsed = 0.0f;
        public float _elapsed2 = 0.0f;

        public int _kills;

        private void Awake()
        {
            I = this;
        }
        private void Start()
        {
            _skill = Resources.Load("Skill") as GameObject;
            _walker = Resources.Load("Walker") as GameObject;

            CreatePoolObject(_walker, 50);
            CreatePoolObject(_skill, 100);

            UIManager.I.Init();
            SkillManager.I.Init();
            LevelManager.I.Init();
            Player.I.Init();

            StartCoroutine(SpawnWalker());
        }
        private void FixedUpdate()
        {
            if (_playing)
            {
                _gameTime += Time.fixedDeltaTime * Time.timeScale;
                _elapsed += Time.fixedDeltaTime * Time.timeScale;
                _elapsed2 += Time.fixedDeltaTime * Time.timeScale;
                if (_elapsed >= 20.0f)
                {
                    _elapsed = 0;
                    _spawnEA *= 2;
                }
                if (_elapsed2 >= 60.0f)
                {
                    _elapsed2 = 0;
                    _enemyHpDeltaT++;
                }
            }
        }

        public void GameOver()
        {
            _playing = false;
            UIManager.I.GameOver();
        }

        public void CreatePoolObject(GameObject pf, int ea)
        {
            Queue<GameObject> pool = null;
            Transform parent = null;

            if (pf.name.Equals("Skill"))
            {
                pool = _poolSkill;
                parent = transform.Find("Pool").Find("Skills");
            }
            else if (pf.name.Equals("Walker"))
            {
                pool = _poolWalker;
                parent = transform.Find("Pool").Find("Enemies").Find("Walker");
            }

            for (int i = 0; i < ea; i++)
            {
                GameObject go = Instantiate(pf);
                if (parent != null)
                    go.transform.parent = parent;
                go.name = i.ToString();
                go.SetActive(false);

                if (pool != null)
                    pool.Enqueue(go);

                else break;
            }
        }
        public GameObject GetPoolObject(GameObject pf)
        {
            GameObject go = null;
            Queue<GameObject> pool = null;

            if (pf.name.Equals("Skill"))
            {
                pool = _poolSkill;
            }
            else if (pf.name.Equals("Walker"))
            {
                pool = _poolWalker;
            }

            if (pool != null)
            {
                if(pool.Count <= 0)
                {
                    CreatePoolObject(pf, 50);
                }
            }

            go = pool.Dequeue();

            return go;
        }
        public void RefillPool(GameObject pf)
        {
            Queue<GameObject> pool = null;

            if (pf.name.Equals("Skill"))
            {
                pool = _poolSkill;
            }
            else if (pf.name.Equals("Walker"))
            {
                pool = _poolWalker;
            }

            if (pool != null)
                pool.Enqueue(pf);
        }

        IEnumerator SpawnWalker()
        {
            while (_playing)
            {
                yield return null;
                if (_enemies.Count >= 80)
                    continue;

                yield return new WaitForSeconds(_spawnCool);

                if (_enemies.Count >= 50)
                    yield return null;
                
                for (int i = 0; i < _spawnEA; i++)
                {
                    yield return new WaitUntil(() => Time.timeScale > 0);

                    if (Player.I != null)
                    {
                        GameObject go = GetPoolObject(_walker);
                        
                        Enemy e = go.GetComponent<Enemy>();
                        e.Init(_enemyHpDeltaT);
                        _enemies.Add(e);
                        Vector2 pos = Player.I.transform.position;

                        List<Vector2> list = new List<Vector2>();

                        Vector2 random = Random.insideUnitCircle * _spawnRange;

                        if (random.x >= 0)
                            random.x = Mathf.Max(random.x, _spawnRange / 2);
                        else
                            random.x = Mathf.Min(random.x, -_spawnRange / 2);
                        if (random.y >= 0)
                            random.y = Mathf.Max(random.y, _spawnRange / 2);
                        else
                            random.y = Mathf.Min(random.y, -_spawnRange / 2);

                        go.transform.position = pos + random;
                        go.SetActive(true);
                    }
                }
            }
        }
    }
}
