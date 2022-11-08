using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance { get; private set; }

    public GameObject[] enemyPrefabs;
    public bool gameOver;
    private float topBound = 8.5f;
    private float bottomBound = -3.0f;
    private float rightSpawn = 27.5f;
    private float spawnDelay = 3;
    private float spawnTimer = 0;
    public int totalKills = 0;
    public int bestKills = 0;


    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver && SceneManager.GetActiveScene().name == "GameScreen")
        {
            CancelInvoke();
            if (bestKills < totalKills)
            {
                bestKills = totalKills;
            }
            SceneManager.LoadScene(2);
        }
        else if (gameOver)
        {
            Cursor.visible = true;
        }
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnDelay)
        {
            SpawnEnemies();
        }
    }

    private Vector3 CreateSpawnPosition()
    {
        float zPos = Random.Range(bottomBound, topBound);
        Vector3 spawnPos = new Vector3(rightSpawn, 0, zPos);
        return spawnPos;
    }

    public void StartGame()
    {
        gameOver = false;
        SceneManager.LoadScene(1);
        spawnDelay = 3;
        spawnTimer = 0;
    }

    private void SpawnEnemies()
    {
        int randomEnemy = Random.Range(0, enemyPrefabs.Length);
        Instantiate(enemyPrefabs[randomEnemy], CreateSpawnPosition(), enemyPrefabs[randomEnemy].transform.rotation);
        spawnDelay *= 0.999f;
        spawnTimer = 0;
    }
}
