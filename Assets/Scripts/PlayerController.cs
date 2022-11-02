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

    [SerializeField] private int maxAmmo = 5;
    private int ammo;

    [SerializeField] TextMeshProUGUI ammoCountText;
    [SerializeField] TextMeshProUGUI moneyCountText;
    
    public static int money = 0;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        ammo = maxAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ammoCountText.text = $"Ammo: {ammo}";
        moneyCountText.text = $"Money: {money}";
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
        }
        else
        {
            //reload when empty and trying to shoot
            ammo = maxAmmo;
        }
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
        maxAmmo = Mathf.FloorToInt((float)(maxAmmo * 1.2));
    }
}
