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

    public static int money;
    public static int maxAmmo;
    public static int ammo;
    public static bool enemyKilled;

    private GameObject playerUnit;
    public GameObject gunMuzzleFire;


    // Start is called before the first frame update
    void Awake()
    {
        Cursor.visible = false;
        spawnManager = SpawnManager.instance;
        money = 0;
        maxAmmo = 5;
        ammo = maxAmmo;
        playerAudio = GetComponent<AudioSource>();
        playerUnit = GameObject.Find("Player Unit");
        gunMuzzleFire.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        if (!spawnManager.gamePaused)
        {
            PlayerUnitAiming();
            UpdateScoreText();
            if (Input.GetMouseButtonDown(0) && transform.position.x > shootMinRange)
            {
                Shoot();
                Debug.DrawLine(Vector3.zero, transform.position, Color.black, 2.5f);
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                ammo = 0;
                StartReloading();
            }
        }
    }

    private void UpdateScoreText()
    {
        if (reloading)
        {
            scoreCountText.text = $"Money: {money}        Kills: {spawnManager.totalKills}      Reloading";
        }
        else
        {
            scoreCountText.text = $"Money: {money}        Kills: {spawnManager.totalKills}      Ammo: {ammo}";
        }
    }

    private void Shoot()
    {
        if (ammo > 0)
        {
            //fire gun when ammo is available
            ammo--;
            gunMuzzleFire.SetActive(true);
            StartCoroutine("GunMuzzleTimer");
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
        else if (!reloading)
        {
            StartReloading();
            //reload when empty and trying to shoot
        }
    }

    IEnumerator GunMuzzleTimer()
    {
        yield return new WaitForSeconds(0.15f);
        gunMuzzleFire.SetActive(false);
    }

    private void StartReloading()
    {
        reloading = true;
        playerAudio.PlayOneShot(reloadSound, 0.5f);
        StartCoroutine("Reload");
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

    private void PlayerUnitAiming()
    {
        float oppLine = transform.position.z - playerUnit.transform.position.z;
        float adjLine = transform.position.x - playerUnit.transform.position.x;

        float aimAngle = Mathf.Atan2(adjLine, oppLine)*Mathf.Rad2Deg;
        Quaternion newRotation = Quaternion.AngleAxis(aimAngle,Vector3.up);
        playerUnit.transform.eulerAngles = new Vector3(0, aimAngle, 0);
    }

}
