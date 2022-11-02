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

    [SerializeField] private int maxAmmo = 5;
    private int ammo;

    [SerializeField] TextMeshProUGUI ammoCountText;

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

    private void onMouseDown()
    {
        Debug.Log("Mouse Clicks");
        if (ammo > 0)
        {
            Shoot();
        }
        else
        {
            Reload();
        }
    }

    public void Shoot()
    {
        ammo--;
    }

    private void Reload()
    {
        //ammo = maxAmmo;
    }

    public void UpgradeAmmo()
    {
        maxAmmo = Mathf.RoundToInt((float)(maxAmmo * 1.5));
    }
}
