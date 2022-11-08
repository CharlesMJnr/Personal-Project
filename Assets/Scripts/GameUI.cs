using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameUI : MonoBehaviour
{
    public GameObject resumeGameButton;
    public GameObject exitGameButton;
    public TextMeshProUGUI pausedGameText;

    private SpawnManager spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = SpawnManager.instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeScale == 0)
        {

            pausedGameText.gameObject.SetActive(true);
            resumeGameButton.SetActive(true);
            exitGameButton.SetActive(true);
        }
        else
        {
            pausedGameText.gameObject.SetActive(false);
            resumeGameButton.SetActive(false);
            exitGameButton.SetActive(false);
        }
    }

    public void ResumeGame()
    {
        spawnManager.PauseGame();
    }

    public void ExitGame()
    {
        spawnManager.Exit();
    }
}
