using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    public float moveTimer = 3.0f;
    public float standTimer = 3.0f;
    private SpawnManager spawnManager;
    private bool moveNow = true;
    public bool gameOver;

    // Start is called before the first frame update
    void Start()
    {
        spawnManager = GameObject.Find("Spawn Manager").GetComponent<SpawnManager>();
        StartCoroutine(AllowMove());
    }

    // Update is called once per frame
    void Update()
    {
        if (moveNow && !spawnManager.gameOver)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if(transform.position.x < -15)
        {
            spawnManager.gameOver = true;
        }

    }

    private void OnMouseDown()
    {
        Destroy(gameObject);
    }

    IEnumerator AllowMove()
    {
        yield return new WaitForSeconds(moveTimer);
        if (!spawnManager.gameOver)
        {
            moveNow = false;
            StartCoroutine(WaitHere());
        }
    }

    IEnumerator WaitHere()
    {
        yield return new WaitForSeconds(standTimer);
        if (!spawnManager.gameOver)
        {
            moveNow = true;
            StartCoroutine(AllowMove());
        }
    }
}
