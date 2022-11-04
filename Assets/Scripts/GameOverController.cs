using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverController : MonoBehaviour
{
    private SpawnManager spawnManager;
    public TextMeshProUGUI bestScoretext;

    private void Start()
    {
        spawnManager = SpawnManager.instance;
        bestScoretext.text = $"Best Score: {spawnManager.bestKills}";
    }

    public void RestartGame()
    {
        spawnManager.StartGame();
    }
}
