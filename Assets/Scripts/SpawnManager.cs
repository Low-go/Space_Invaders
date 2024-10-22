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
    private int waveCount = 0;

    private void Start()
    {

    }

    private IEnumerator SpawnEnemies()
    {
        yield return new WaitForSeconds(startDelay);

        while (isSpawning)
        {
            int enemyCount = Random.Range(4 + waveCount, 8 + waveCount); // 4-8 enemies plus wave count
            SpawnEnemiesAtRandom(enemyCount);

            waveCount++; // Increase wave count for next spawn
            float spawnInterval = Random.Range(5, 10); // Random time for each spawn
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnEnemiesAtRandom(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPos = new Vector3(Random.Range(-spawnRange, spawnRange), 0.573f, spawnPosZ); // Random x location
            GameObject enemy = Instantiate(enemyPrefab, spawnPos, enemyPrefab.transform.rotation); // Instantiate

            // Randomize enemy speed
            float speed = Random.Range(1f, 5f); // Adjust the speed range as needed
            EnemyMove enemyMove = enemy.GetComponent<EnemyMove>(); // Get the EnemyMove component

            if (enemyMove != null) // Check if component exists
            {
                enemyMove.SetSpeed(speed); // Set the enemy's speed
            }
            else
            {
                Debug.LogWarning("EnemyMove component not found on the enemy prefab!");
            }
        }
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

    // Reset wave count and any other necessary variables
    public void ResetSpawner()
    {
        waveCount = 0; // Reset wave count to initial value
        isSpawning = false; // Stop spawning if necessary
        StopSpawning(); // Ensure all enemies are removed
    }
}
