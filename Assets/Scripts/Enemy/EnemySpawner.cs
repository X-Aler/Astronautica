using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    EnvironmentController environmentController;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject[] enemySpawnPos;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private void Awake()
    {
        environmentController = FindAnyObjectByType<EnvironmentController>();
    }
    private void OnEnable()
    {
        environmentController.onNightStarted += Spawn;
        environmentController.onDayStarted += Despawn;
    }

    private void OnDisable()
    {
        environmentController.onNightStarted -= Spawn;
        environmentController.onDayStarted -= Despawn;
    }

    private void Spawn()
    {
        foreach (var item in enemySpawnPos)
        {
            GameObject a = Instantiate(enemyPrefab, null);
            spawnedEnemies.Add(a);
            a.transform.position = item.transform.position;
        }
    }

    private void Despawn()
    {
        for (int i = spawnedEnemies.Count - 1; i >= 0; i--)
        {
            GameObject enemy = spawnedEnemies[i];
            if (enemy != null)
            {
                bool nearSpawn = false;
                foreach (var spawn in enemySpawnPos)
                {
                    if (Vector2.Distance(enemy.transform.position, spawn.transform.position) <= 10)
                    {
                        nearSpawn = true;
                        break;
                    }
                }

                if (nearSpawn)
                {
                    Destroy(enemy);
                    spawnedEnemies.RemoveAt(i);
                }
            }
            else
            {
                spawnedEnemies.RemoveAt(i);
            }
        }
    }
}
