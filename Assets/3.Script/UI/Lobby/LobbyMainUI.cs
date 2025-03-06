using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private GameObject equipmentPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject ManaStonePanel;

    public void ClickEquipment()
    {
        gameObject.SetActive(false);

        equipmentPanel.SetActive(true);
    }
    public void ClickInventory()
    {
        inventoryPanel.SetActive(true);
    }
    public void ClickStart()
    {
        EquipmentManager temp = EquipmentManager.instance;
        if (temp.EquippedWeapon != null && temp.EquippedArmor != null &&
           temp.EquippedBoots != null && temp.usingSkill != null)
        {
            SceneManager.LoadScene("MainGame");
        }
    }
    public void ManaStoneButton()
    {
        gameObject.SetActive(false);

        ManaStonePanel.SetActive(true);
    }
}
