using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject SpiderPrefab;
    public GameObject PurpleAlienPrefab;
    public GameObject Alien1Prefab;
    public GameObject Spawner;

    public float SpiderInterval = 5f;
    public float PurpleAlienInterval = 10f;
    public float Alien1Interval = 20f;
    
    public int PoolSize = 10;    // size of the enemy pool

    private Queue<GameObject> _spiderPool;
    private Queue<GameObject> _purpleAlienPool;
    private Queue<GameObject> _alien1Pool;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the enemy pools
        _spiderPool = new Queue<GameObject>(PoolSize);
        _purpleAlienPool = new Queue<GameObject>(PoolSize);
        _alien1Pool = new Queue<GameObject>(PoolSize);

        // Fill the pools with inactive enemy prefabs
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject spider = Instantiate(SpiderPrefab, Vector3.zero, Quaternion.identity);
            spider.SetActive(false);
            _spiderPool.Enqueue(spider);

            GameObject purpleAlien = Instantiate(PurpleAlienPrefab, Vector3.zero, Quaternion.identity);
            purpleAlien.SetActive(false);
            _purpleAlienPool.Enqueue(purpleAlien);

            GameObject alien1 = Instantiate(Alien1Prefab, Vector3.zero, Quaternion.identity);
            alien1.SetActive(false);
            _alien1Pool.Enqueue(alien1);
        }

        // Start the enemy spawning coroutines
        StartCoroutine(SpawnEnemy(SpiderInterval, _spiderPool, SpiderPrefab));
        StartCoroutine(SpawnEnemy(PurpleAlienInterval, _purpleAlienPool, PurpleAlienPrefab));
        StartCoroutine(SpawnEnemy(Alien1Interval, _alien1Pool, Alien1Prefab));
    }

    private IEnumerator SpawnEnemy(float interval, Queue<GameObject> enemyPool, GameObject enemyPrefab)
    {
        yield return new WaitForSeconds(interval);

        // Get the next enemy from the pool
        GameObject enemy = enemyPool.Dequeue();

        // If the pool is empty, add another enemy to it
        if (enemy == null)
        {
            enemy = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity);
        }

        // Set the enemy's position and activate it
        enemy.transform.position = new Vector3(Random.Range(Spawner.transform.position.x - 1f, Spawner.transform.position.x + 1f),
                                               Random.Range(Spawner.transform.position.y - 1f, Spawner.transform.position.y + 1f),
                                               0f);
        enemy.SetActive(true);

        // Add the enemy back to the pool
        enemyPool.Enqueue(enemy);

        StartCoroutine(SpawnEnemy(interval, enemyPool, enemyPrefab));
    }
}
