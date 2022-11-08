using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class AmmoUpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI descriptionText;
    public static int upgradePrice = 5;
    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionText.text = $"Upgrades Max ammo per clip. Current Cost: {upgradePrice}";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionText.text = "";
    }

    public void UpgradePlayerAmmo()
    {
        if (PlayerController.money >= upgradePrice)
        {
            PlayerController.money -= upgradePrice;
            PlayerController.maxAmmo = Mathf.CeilToInt((float)(PlayerController.maxAmmo * 1.2));
            upgradePrice = Mathf.CeilToInt((float)(upgradePrice * 1.5));
        }
    }
}
