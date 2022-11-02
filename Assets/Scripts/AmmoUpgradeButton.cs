using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class AmmoUpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] TextMeshProUGUI descriptionText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        descriptionText.text = $"Upgrades Max ammo per clip. Current Cost: {PlayerController.maxAmmo}";
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        descriptionText.text = "";
    }
}
