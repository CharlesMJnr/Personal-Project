using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public bool gameOver;
    private float topBound = 8.5f;
    private float bottomBound = -3.0f;
    private float rightSpawn = 27.5f;
    private float startDelay = 2.0f;
    private float spawnDelay = 2f;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("StartEnemies", startDelay, spawnDelay);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartEnemies()
    {
        int randomEnemy = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[randomEnemy], CreateSpawnPosition(), enemyPrefabs[randomEnemy].transform.rotation);
    }

    private Vector3 CreateSpawnPosition()
    {
        float zPos = Random.Range(bottomBound, topBound);
        Vector3 spawnPos = new Vector3(rightSpawn, 0, zPos);
        return spawnPos;
    }
}
