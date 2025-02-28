using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LobbyUI : MonoBehaviour
{
    [SerializeField] private GameObject equipmentPanel;
    [SerializeField] private GameObject inventoryPanel;

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
        if (EquipmentManager.instance.EquippedWeapon != null&&
            EquipmentManager.instance.EquippedArmor != null&&
            EquipmentManager.instance.EquippedBoots != null)
        {
            SceneManager.LoadScene("MainGame");
        }
    }
}
