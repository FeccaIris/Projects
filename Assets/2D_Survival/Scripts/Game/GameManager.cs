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

        public Queue<GameObject> _poolProj = new Queue<GameObject>();
        public Queue<GameObject> _poolWalker = new Queue<GameObject>();
        public GameObject _proj;
        public GameObject _walker;


        public List<Enemy> _enemies;

        bool _playing = true;

        float _spawnCool = 3.0f;
        float _spawnRange = 60.0f;
        
        public float _gameTime = 0.0f;

        private void Awake()
        {
            I = this;
        }
        private void Start()
        {
            _proj = Resources.Load("SV_Projectile") as GameObject;
            _walker = Resources.Load("Walker") as GameObject;

            CreatePoolObject(_walker, 50);
            CreatePoolObject(_proj, 10);

            UIManager.I.Init();
            SkillManager.I.Init();
            LevelManager.I.Init();
            Player.I.Init();

            StartCoroutine(SpawnEnemy());
        }
        private void FixedUpdate()
        {
            _gameTime += Time.fixedDeltaTime * Time.timeScale;
        }

        public void CreatePoolObject(GameObject pf, int ea)
        {
            Queue<GameObject> pool = null;
            Transform parent = null;

            Enemy e = pf.GetComponent<Enemy>();
            if(e != null)
            {
                if (e is Walker)
                {
                    pool = _poolWalker;
                    parent = transform.Find("Pool").Find("Enemies").Find("Walker");
                }
            }
            else
            {
                Skill sk = pf.GetComponent<Skill>();
                if(sk != null)
                {
                    if (sk is Projectile)
                    {
                        pool = _poolProj;
                        parent = transform.Find("Pool").Find("Skills").Find("Projectile");
                    }
                }
            }

            for (int i = 0; i < ea; i++)
            {
                GameObject go = Instantiate(pf);
                if (parent != null)
                    go.transform.parent = parent;

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

            Enemy e = pf.GetComponent<Enemy>();
            if (e != null)
            {
                if (e is Walker)
                    pool = _poolWalker;
            }
            else
            {
                Skill sk = pf.GetComponent<Skill>();
                if (sk != null)
                {
                    if (sk is Projectile)
                        pool = _poolProj;
                }
            }
            if (pool != null)
            {
                if(pool.Count <= 0)
                {
                    CreatePoolObject(pf, 5);
                }
            }

            go = pool.Dequeue();
            go.SetActive(true);

            return go;
        }
        public void RefillPool(GameObject pf)
        {
            Queue<GameObject> pool = null;

            Enemy e = pf.GetComponent<Enemy>();
            if (e != null)
            {
                if (e is Walker)
                    pool = _poolWalker;
            }
            else
            {
                Skill sk = pf.GetComponent<Skill>();
                if (sk != null)
                {
                    if (sk is Projectile)
                        pool = _poolProj;
                }
            }

            if (pool != null)
                pool.Enqueue(pf);
        }

        IEnumerator SpawnEnemy()
        {
            while (_playing)
            {
                yield return new WaitForSeconds(_spawnCool * Time.timeScale);

                if (Player.I != null)
                {
                    GameObject go = GetPoolObject(_walker);
                    Enemy e = go.GetComponent<Enemy>();
                    _enemies.Add(e);
                    Vector2 pos = Player.I.transform.position;
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
                }
            }
        }
    }
}
