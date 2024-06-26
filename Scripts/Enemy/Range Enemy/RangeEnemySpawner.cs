using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemySpawner : MonoBehaviour {

    // Class for EnemySpawner info and Prefabs
    [System.Serializable]
    public class RangeEnemy {
        public List<GameObject> enemyPrefabs;
        public int enemySpawned = 0;
        public int enemySpawnedCap;
    }
    // List with class Enemy 
    public List<RangeEnemy> rangeEnemyList;
    public Transform playerTransform; // Reference to the player's transform

    // Timer for spawner
    private float spawnerTimer = 0.8f;
    private float timeSinceLastSpawn = 0f;


    void Update() {
        // Timer logic for spawning enemies
        timeSinceLastSpawn += Time.deltaTime;
        if (timeSinceLastSpawn >= spawnerTimer)
        {
            if (AnyEnemyNeedsSpawning())
            {
                SpawnEnemy();
                timeSinceLastSpawn = 0f; // Reset the timer when an enemy is spawned
            }
        }

        // For demonstration, call SpawnEnemy on some condition, e.g., a key press
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            SpawnEnemy();
        }
    }
    bool AnyEnemyNeedsSpawning() {
        foreach (var enemy in rangeEnemyList)
        {
            if (enemy.enemySpawned < enemy.enemySpawnedCap)
            {
                return true;
            }
        }
        return false;
    }

    public void SpawnEnemy() {
        foreach (var enemy in rangeEnemyList)
        {
            if (enemy.enemySpawned < enemy.enemySpawnedCap)
            {
                // Create a random number between 0 and the count of enemyPrefabs
                int randomIndex = Random.Range(0, enemy.enemyPrefabs.Count);

                // Instantiate the enemy at a random position close to the player
                Vector3 spawnPosition = GetRandomPositionNearPlayer();

                GameObject newEnemy = Instantiate(enemy.enemyPrefabs[randomIndex], spawnPosition, Quaternion.identity);

                // Make the spawned enemy a child of the Spawner Manager object
                newEnemy.transform.parent = transform;

                // Increment the enemySpawned count
                enemy.enemySpawned++;
            }
        }
    }

    Vector3 GetRandomPositionNearPlayer() {
        float distance = 5.0f; // Set the minimum distance away from the player
        Vector3 randomDirection = Random.insideUnitCircle.normalized * distance;
        Vector3 spawnPosition = playerTransform.position + new Vector3(randomDirection.x, randomDirection.y, 0);
        return spawnPosition;
    }

    // Call this method when an enemy dies
    public void OnEnemyDeath() {
        foreach (var enemy in rangeEnemyList)
        {
            enemy.enemySpawned--;
        }


    }
}
