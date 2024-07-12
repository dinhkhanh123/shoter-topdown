using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private float secondsBetweenSpawns;
    float secondsSinceLastSpawn;

    [SerializeField] private int enemiesToSpawn;

    private void OnEnable()
    {
        Reference.spawner.Add(this);
    }

    private void OnDisable()
    {
        Reference.spawner.Remove(this);

    }

    void Start()
    {
        secondsSinceLastSpawn = 0;
    }
    //Fixed update happens the same number of times for all players, so it's a good place for gameplay critical things
    void FixedUpdate()
    {
        if (Reference.levelManager.alarmSounded && enemiesToSpawn > 0)
        {
            secondsSinceLastSpawn += Time.fixedDeltaTime;
            if (secondsSinceLastSpawn >= secondsBetweenSpawns)
            {
                Instantiate(enemyPrefab, spawnPoint.transform.position, Quaternion.identity);
                secondsSinceLastSpawn = 0;
                enemiesToSpawn--;
            }
        }
       
    }
}
