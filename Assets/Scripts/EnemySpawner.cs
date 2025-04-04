using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnDistance = 5f;
    public float spawnInterval = 120f;
    GameObject player;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(SpawnEnemyRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnInterval);
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        if (enemyPrefab == null)
        {
            Debug.LogError("Enemy Prefab is not assigned!");
            return;
        }

        if (LevelManager.isPlaying == false)
        {
            Debug.Log("Game is not playing. Skipping spawn.");
            return;
        }

        if (player == null || !player.activeInHierarchy)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            if (player == null || !player.activeInHierarchy)
            {
                Debug.Log("No player. Skipping spawn.");
                return;
            }
        }

        Vector3 spawnPosition = player.transform.position - player.transform.forward * spawnDistance;

        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }
}
