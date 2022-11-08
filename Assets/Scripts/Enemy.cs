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
    private bool dying = false;
    private Animator enemyAnim;

    private float lyingFlatZ = -0.5f;
    private Vector3 rotationSpeedVector = new Vector3(0, 0, -0.4f);


    // Start is called before the first frame update
    void Start()
    {
        spawnManager = SpawnManager.instance;
        StartCoroutine(AllowMove());
        enemyAnim = this.GetComponentInChildren<Animator>();
        enemyAnim.SetFloat("Speed_f", 0.5f);
        enemyAnim.SetBool("Static_b", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (moveNow && !spawnManager.gameOver && !dying)
        {
            transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        else if (dying)
        {
            DeathMovement();
        }
    }

    private void DeathMovement()
    {
        transform.Rotate(rotationSpeedVector);
        if (transform.rotation.z <= lyingFlatZ)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseDown()
    {
        if (PlayerController.ammo > 0 && !dying)
        {
            PlayerController.money++;
            spawnManager.totalKills++;
            dying = true;
            enemyAnim.SetFloat("Speed_f", 0);
            enemyAnim.SetBool("Static_b", true);
        }
        PlayerController.enemyKilled = true;
    }

    IEnumerator AllowMove()
    {
        yield return new WaitForSeconds(moveTimer);
        if (!spawnManager.gameOver && !dying)
        {
            moveNow = false;
            enemyAnim.SetFloat("Speed_f", 0);
            enemyAnim.SetBool("Static_b", true);
            StartCoroutine(WaitHere());
        }
    }

    IEnumerator WaitHere()
    {
        yield return new WaitForSeconds(standTimer);
        if (!spawnManager.gameOver && !dying)
        {
            moveNow = true;
            enemyAnim.SetFloat("Speed_f", 0.5f);
            enemyAnim.SetBool("Static_b", false);
            StartCoroutine(AllowMove());
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Wall"))
        {
            spawnManager.gameOver = true;
            Destroy(gameObject);
        }
    }
}
