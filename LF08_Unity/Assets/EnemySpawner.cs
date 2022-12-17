using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spiderPrefab;
    public GameObject purpleAlienPrefab;
    public GameObject alien1Prefab;
    public GameObject spawner;

    public float SpiderInterval = 5f;
    public float PurpleAlienInterval = 10f;
    public float Alien1Interval = 20f;
    
    public int PoolSize = 10;    // size of the enemy pool

    private Queue<GameObject> spiderPool;
    private Queue<GameObject> purpleAlienPool;
    private Queue<GameObject> alien1Pool;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the enemy pools
        spiderPool = new Queue<GameObject>(PoolSize);
        purpleAlienPool = new Queue<GameObject>(PoolSize);
        alien1Pool = new Queue<GameObject>(PoolSize);

        // Fill the pools with inactive enemy prefabs
        for (int i = 0; i < PoolSize; i++)
        {
            GameObject spider = Instantiate(spiderPrefab, Vector3.zero, Quaternion.identity);
            spider.SetActive(false);
            spiderPool.Enqueue(spider);

            GameObject purpleAlien = Instantiate(purpleAlienPrefab, Vector3.zero, Quaternion.identity);
            purpleAlien.SetActive(false);
            purpleAlienPool.Enqueue(purpleAlien);

            GameObject alien1 = Instantiate(alien1Prefab, Vector3.zero, Quaternion.identity);
            alien1.SetActive(false);
            alien1Pool.Enqueue(alien1);
        }

        // Start the enemy spawning coroutines
        StartCoroutine(SpawnEnemy(SpiderInterval, spiderPool, spiderPrefab));
        StartCoroutine(SpawnEnemy(PurpleAlienInterval, purpleAlienPool, purpleAlienPrefab));
        StartCoroutine(SpawnEnemy(Alien1Interval, alien1Pool, alien1Prefab));
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
        enemy.transform.position = new Vector3(Random.Range(spawner.transform.position.x - 1f, spawner.transform.position.x + 1f),
                                               Random.Range(spawner.transform.position.y - 1f, spawner.transform.position.y + 1f),
                                               0f);
        enemy.SetActive(true);

        // Add the enemy back to the pool
        enemyPool.Enqueue(enemy);

        StartCoroutine(SpawnEnemy(interval, enemyPool, enemyPrefab));
    }
}
