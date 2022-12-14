using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject SpiderPrefab;
    public GameObject PurpleAlienPrefab;
    public GameObject Alien1Prefab;
    public GameObject Spawner;

    public float SpiderInterval = 6f;
    public float PurpleAlienInterval = 20f;
    public float Alien1Interval = 30f;
    
    public int PoolSize = 5;    // size of the enemy pool

    private List<GameObject> _spiderPool;
    private List<GameObject> _purpleAlienPool;
    private List<GameObject> _alien1Pool;

    // Pre-determined positions for enemies to spawn at
    private Vector3[] _spawnPositions;

    /// <summary>
    /// Initializes the enemy pools and spawn positions, and starts the enemy spawning coroutine.
    /// </summary>
    void Start()
    {
        // Initialize the enemy pools
        _spiderPool = new List<GameObject>(PoolSize);
        _purpleAlienPool = new List<GameObject>(PoolSize);
        _alien1Pool = new List<GameObject>(PoolSize);

        // Pre-populate the enemy pools with inactive enemy prefabs
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject spider = Instantiate(SpiderPrefab, Vector3.zero, Quaternion.identity);
            spider.SetActive(false);
            _spiderPool.Add(spider);

            GameObject purpleAlien = Instantiate(PurpleAlienPrefab, Vector3.zero, Quaternion.identity);
            purpleAlien.SetActive(false);
            _purpleAlienPool.Add(purpleAlien);

            GameObject alien1 = Instantiate(Alien1Prefab, Vector3.zero, Quaternion.identity);
            alien1.SetActive(false);
            _alien1Pool.Add(alien1);
        }

        // Initialize the spawn positions array
        _spawnPositions = new Vector3[]
        {
            new(Spawner.transform.position.x - 1f, Spawner.transform.position.y - 1f, 0f),
            new(Spawner.transform.position.x + 1f, Spawner.transform.position.y - 1f, 0f),
            new(Spawner.transform.position.x - 1f, Spawner.transform.position.y + 1f, 0f),
            new(Spawner.transform.position.x + 1f, Spawner.transform.position.y + 1f, 0f)
        };

        // Start the enemy spawning coroutine
        StartCoroutine(SpawnEnemySpider());
        StartCoroutine(SpawnEnemyPurpleAlien());
        StartCoroutine(SpawnEnemyAlien1());
    }

    /// <summary>
    /// Coroutine that spawns enemies at intervals.
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnEnemySpider()
    {
        yield return new WaitForSeconds(SpiderInterval);

        // Get the next spider from the pool
        GameObject spider = GetNextEnemy(_spiderPool);

        // Set the spider's position and activate it
        spider.transform.position = GetRandomSpawnPosition();
        spider.SetActive(true);

        // Add the spider back to the pool
        _spiderPool.Add(spider);

        if (SpiderInterval > 0.1f) SpiderInterval -= 0.2f;

        StartCoroutine(SpawnEnemySpider());
    }

    private IEnumerator SpawnEnemyPurpleAlien()
    {
        yield return new WaitForSeconds(PurpleAlienInterval);

        // Get the next purple alien from the pool
        GameObject purpleAlien = GetNextEnemy(_purpleAlienPool);

        // Set the purple alien's position and activate it
        purpleAlien.transform.position = GetRandomSpawnPosition();
        purpleAlien.SetActive(true);

        // Add the purple alien back to the pool
        _purpleAlienPool.Add(purpleAlien);

        if (PurpleAlienInterval > 0.1f) PurpleAlienInterval -= 0.2f;

        StartCoroutine(SpawnEnemyPurpleAlien());
    }

    private IEnumerator SpawnEnemyAlien1()
    {
        yield return new WaitForSeconds(Alien1Interval);

        // Get the next alien1 from the pool
        GameObject alien1 = GetNextEnemy(_alien1Pool);

        // Set the alien1's position and activate it
        alien1.transform.position = GetRandomSpawnPosition();
        alien1.SetActive(true);

        // Add the alien1 back to the pool
        _alien1Pool.Add(alien1);

        if (Alien1Interval > 0.1f) Alien1Interval -= 0.2f;

        StartCoroutine(SpawnEnemyAlien1());
    }

    /// <summary>
    /// Returns the next enemy from the specified pool, or instantiates a new one if the pool is empty
    /// </summary>
    /// <param name="enemyPool"></param>
    /// <returns></returns>
    private GameObject GetNextEnemy(IList<GameObject> enemyPool)
    {
        if (enemyPool.Count > 0)
        {
            GameObject enemy = enemyPool[0];
            enemyPool.RemoveAt(0);
            return enemy;
        }
        else
        {
            // Return a new enemy if the pool is empty
            return Instantiate(SpiderPrefab, Vector3.zero, Quaternion.identity);
        }
    }

    /// <summary>
    /// Returns a random spawn position from the spawn positions array.
    /// </summary>
    /// <returns>A random spawn position.</returns>
    private Vector3 GetRandomSpawnPosition()
    {
        return _spawnPositions[Random.Range(0, _spawnPositions.Length)];
    }
    
}