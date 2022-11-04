using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Vector3 screenPosition;
    public Vector3 worldPosition;

    private float leftXBound = -12f;
    private float rightXbound = 17.5f;
    private float topZBound = 8.5f;
    private float bottomZbound = -3f;
    private float cameraToPlaneDist = 10;
    private float shootMinRange = -8;
    private SpawnManager spawnManager;

    private bool reloading = false;
    private AudioSource playerAudio;

    [SerializeField] TextMeshProUGUI ammoCountText;
    [SerializeField] TextMeshProUGUI scoreCountText;
    [SerializeField] GameObject upgradeAmmoButton;
    public AudioClip gunShotSound;
    public AudioClip reloadSound;
    public AudioClip gunMissSound;
    
    public static int money = 0;
    public static int maxAmmo = 5;
    public static int ammo;
    public static bool enemyKilled;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        spawnManager = SpawnManager.instance;
        ammo = maxAmmo;
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        scoreCountText.text = $"Money: {money}        Kills: {spawnManager.totalKills}";
        if (reloading)
        {
            ammoCountText.text = "Reloading";
        }
        else
        {
        ammoCountText.text = $"Ammo: {ammo}";
        }
        if (Input.GetMouseButtonDown(0) && transform.position.x > shootMinRange)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if(ammo > 0)
        {
            //fire gun when ammo is available
            ammo--;
            if (enemyKilled)
            {
                playerAudio.PlayOneShot(gunShotSound, 0.5f);
                enemyKilled = false;
            }
            else
            {
                playerAudio.PlayOneShot(gunMissSound, 0.5f);
            }
        }
        else if(!reloading)
        {
            reloading = true;
            playerAudio.PlayOneShot(reloadSound, 0.5f);
            StartCoroutine("Reload");
            //reload when empty and trying to shoot
        }
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(2);
        ammo = maxAmmo;
        reloading = false;
    }

    void MovePlayer()
    {
        //Tracks the location of the mouse inside the gameScreen and moves the scope within the bounds of the playable within based on the mouse.
        screenPosition = Input.mousePosition;
        screenPosition.z = Camera.main.nearClipPlane + cameraToPlaneDist;

        worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        if (worldPosition.x > leftXBound && worldPosition.x < rightXbound && worldPosition.z > bottomZbound && worldPosition.z < topZBound)
        {
            Cursor.visible = false;
            transform.position = worldPosition;
        }
        else
        {
            Cursor.visible = true;
        }
    }

    public void UpgradeAmmo()
    {
        if (money >= maxAmmo)
        {
            money -= maxAmmo;
            maxAmmo = Mathf.FloorToInt((float)(maxAmmo * 1.2));
        }
    }


}
