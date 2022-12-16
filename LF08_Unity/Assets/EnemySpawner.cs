using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spiderPrefab;
    public GameObject purpleAlienPrefab;
    public GameObject alien1Prefab;
    public GameObject spawner;

    public float interval = 5f;  // fixed interval for spawning enemies
    public int poolSize = 10;    // size of the enemy pool

    private Queue<GameObject> spiderPool;
    private Queue<GameObject> purpleAlienPool;
    private Queue<GameObject> alien1Pool;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the enemy pools
        spiderPool = new Queue<GameObject>(poolSize);
        purpleAlienPool = new Queue<GameObject>(poolSize);
        alien1Pool = new Queue<GameObject>(poolSize);

        // Fill the pools with inactive enemy prefabs
        for (int i = 0; i < poolSize; i++)
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
        StartCoroutine(spawnEnemy(interval, spiderPool, spiderPrefab));
        StartCoroutine(spawnEnemy(interval, purpleAlienPool, purpleAlienPrefab));
        StartCoroutine(spawnEnemy(interval, alien1Pool, alien1Prefab));
    }

    private IEnumerator spawnEnemy(float interval, Queue<GameObject> enemyPool, GameObject enemyPrefab)
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

        StartCoroutine(spawnEnemy(interval, enemyPool, enemyPrefab));
    }
}
