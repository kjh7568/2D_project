using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyEquipmentUI : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject equipmentGachaPanel;
    [SerializeField] private GameObject equipmentEnhancementPanel;
    public void Exit()
    {
        gameObject.SetActive(false);

        mainPanel.SetActive(true);
    }

    public void EquipmentGachaButton()
    {
        gameObject.SetActive(false);

        equipmentGachaPanel.SetActive(true);
    }

    public void EquipmentEnhancementButton()
    {
        gameObject.SetActive(false);

        equipmentEnhancementPanel.SetActive(true);
    }
}
