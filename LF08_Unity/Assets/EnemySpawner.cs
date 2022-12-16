using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject spiderPrefab;
    public float spiderInterval = 5f;

    public GameObject purpleAlienPrefab;
    public float purpleAlienInterval = 12f;

    public GameObject alien1Prefab;
    public float alien1Interval = 25f;

    public GameObject spawner;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(spawnEnemy(spiderInterval, spiderPrefab));
        StartCoroutine(spawnEnemy(purpleAlienInterval, purpleAlienPrefab));
        StartCoroutine(spawnEnemy(alien1Interval, alien1Prefab));
    }

    private IEnumerator spawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(interval);

        GameObject newEnemy = Instantiate(enemy, 
                              new Vector3(Random.Range(spawner.transform.position.x - 1f, spawner.transform.position.x + 1f), 
                                          Random.Range(spawner.transform.position.y - 1f, spawner.transform.position.y + 1f),
                                          0f) ,Quaternion.identity);

        StartCoroutine(spawnEnemy(interval, enemy));
    }
}
