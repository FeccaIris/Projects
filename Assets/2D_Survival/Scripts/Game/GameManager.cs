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
        public Queue<GameObject> _poolCharger = new Queue<GameObject>();

        public GameObject _skill;
        public GameObject _walker;
        public GameObject _charger;

        public List<Enemy> _enemies;

        public bool _isPlaying = false;

        float _spawnRange = 60.0f;
        float _walkerCool = 3.0f;
        float _chargerCool = 6.0f;
        float _walkerEA = 1;
        float _chargerEA = 1;
        float _enemyHpDeltaT;

        int _maxEnemies = 500;
        
        public float _gameTime = 0.0f;
        public float _elapsed = 0.0f;
        public float _elapsed2 = 0.0f;
        public float _elapsed3 = 0.0f;

        public int _kills;

        private void Awake()
        {
            I = this;
        }
        private void Start()
        {
            _skill = Resources.Load("Skill") as GameObject;
            _walker = Resources.Load("Walker") as GameObject;
            _charger = Resources.Load("Charger") as GameObject;

            CreatePoolObject(_walker, 50);
            CreatePoolObject(_skill, 100);
            CreatePoolObject(_charger, 50);

            UIManager.I.Init();
            SkillManager.I.Init();
            LevelManager.I.Init();
            Player.I.Init();

            StartCoroutine(SpawnWalker());
            StartCoroutine(SpawnCharger());

            _isPlaying = true;
        }
        private void FixedUpdate()
        {
            if (_isPlaying)
            {
                _gameTime += Time.fixedDeltaTime * Time.timeScale;
                _elapsed += Time.fixedDeltaTime * Time.timeScale;
                _elapsed2 += Time.fixedDeltaTime * Time.timeScale;
                _elapsed3 += Time.fixedDeltaTime * Time.timeScale;
                if (_elapsed >= 20.0f)
                {
                    _elapsed = 0;
                    _walkerEA *= 2;
                    _chargerEA++;
                }
                if (_elapsed2 >= 10.0f)
                {
                    _elapsed2 = 0;
                    _enemyHpDeltaT += 2;
                }
                if (_elapsed3 >= 40.0f)
                {
                    _elapsed3 = 0;
                    _chargerEA++;
                }
                if (_gameTime >= 60 * 5)
                {
                    GameClear();
                }
            }
        }

        public void GameOver()
        {
            _isPlaying = false;
            UIManager.I.GameOver();
        }
        public void GameClear()
        {
            _isPlaying = false;
            UIManager.I.GameOver(true);
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
            else if (pf.name.Equals("Charger"))
            {
                pool = _poolCharger;
                parent = transform.Find("Pool").Find("Enemies").Find("Charger");
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
            else if (pf.name.Equals("Charger"))
            {
                pool = _poolCharger;
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
            else if (pf.name.Equals("Charger"))
            {
                pool = _poolCharger;
            }

            if (pool != null)
                pool.Enqueue(pf);
        }

        IEnumerator SpawnWalker()
        {
            while (_isPlaying == true)
            {
                if (_enemies.Count >= _maxEnemies)
                    yield return new WaitUntil(() => _enemies.Count < _maxEnemies);

                yield return new WaitForSeconds(_walkerCool);

                Vector2 random = new Vector2();

                for (int i = 0; i < _walkerEA; i++)
                {
                    yield return new WaitUntil(() => Time.timeScale > 0);

                    if (Player.I != null)
                    {
                        GameObject go = GetPoolObject(_walker);
                        
                        Enemy e = go.GetComponent<Enemy>();
                        e.Init(_enemyHpDeltaT);
                        _enemies.Add(e);
                        Vector2 pos = Player.I.transform.position;

                        while (true)
                        {
                            Vector2 t = Random.insideUnitCircle * _spawnRange;
                            if(random != t)
                            {
                                random = t;
                                break;
                            }
                        }

                        if (random.x >= 0)
                            random.x = Mathf.Max(random.x, random.x + _spawnRange / 2);
                        else
                            random.x = Mathf.Min(random.x, random.x + -_spawnRange / 2);
                        if (random.y >= 0)
                            random.y = Mathf.Max(random.y, random.y + _spawnRange / 2);
                        else
                            random.y = Mathf.Min(random.y, random.y + - _spawnRange / 2);

                        go.transform.position = pos + random;
                        go.SetActive(true);
                    }
                }
            }
        }
        IEnumerator SpawnCharger()
        {
            while (_isPlaying == true)
            {
                yield return new WaitUntil(() => _gameTime >= 20.0f);

                if (_enemies.Count >= _maxEnemies)
                    yield return new WaitUntil(() => _enemies.Count <= _maxEnemies);

                yield return new WaitForSeconds(_chargerCool);

                for (int i = 0; i < _chargerEA; i++)
                {
                    yield return new WaitUntil(() => Time.timeScale > 0);

                    if (Player.I != null)
                    {
                        Vector2 r = new Vector2();
                        for (int j = 0; j < 4; j++)
                        {
                            GameObject go = GetPoolObject(_charger);

                            Enemy e = go.GetComponent<Enemy>();
                            e.Init(_enemyHpDeltaT);
                            _enemies.Add(e);
                            Vector2 pos = Player.I.transform.position;

                            Vector2 sPos = new Vector2();

                            while (true)
                            {
                                Vector2 t = Random.insideUnitCircle;
                                if( r != t)
                                {
                                    r = t;
                                    break;
                                }
                                yield return null;
                            }

                            if (r.x >= 0)
                                r.x = Mathf.Max(r.x, 2);
                            else
                                r.x = Mathf.Min(r.x, -2);
                            if (r.y >= 0)
                                r.y = Mathf.Max(r.y, 2);
                            else
                                r.y = Mathf.Min(r.y, -2);

                            r *= 5;
                            switch (j)
                            {
                                case 0:
                                    {
                                        sPos = new Vector2(0, 50) + r;
                                        break;
                                    }
                                case 1:
                                    {
                                        sPos = new Vector2(0, -50) + r;
                                        break;
                                    }
                                case 2:
                                    {
                                        sPos = new Vector2(80, 0) + r;
                                        break;
                                    }
                                case 3:
                                    {
                                        sPos = new Vector2(-80, 0) + r;
                                        break;
                                    }
                            }

                            go.transform.position = pos + sPos;

                            go.SetActive(true);
                        }
                    }
                }
            }
        }
    }
}
