using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Assign enemy to this
    private float spawnRange = 9;
    private float startDelay = 3;
    private float spawnPosZ = 10.3f;
    private Coroutine spawnRoutine;
    private bool isSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(startDelay);

        while (isSpawning)
        {
            SpawnEnemiesAtRandom();
            float spawnInterval = Random.Range(5, 10); // New random time for each spawn
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemiesAtRandom()
    {
        Vector3 spawnPos = new Vector3(Random.Range(-spawnRange, spawnRange), 0.573f, spawnPosZ); // Random x location
        Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation); // Instantiate
    }

    public void StartSpawning()
    {
        isSpawning = true;
        StartCoroutine(SpawnEnemies());
    }

    public void StopSpawning()
    {
        isSpawning = false;
        if (spawnRoutine != null)
        {
            StopCoroutine(spawnRoutine);
        }

        // Destroy all enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
    }

}