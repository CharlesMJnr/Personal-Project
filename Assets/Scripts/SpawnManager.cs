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
    private float startDelay = 2.0f;
    private float spawnDelay = 2f;
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

    public void StartGame()
    {
        gameOver = false;
        SceneManager.LoadScene(1);
        InvokeRepeating("StartEnemies", startDelay, spawnDelay);
    }
}
