using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentEnchant : MonoBehaviour
{
    [SerializeField] private GameObject mainPanel;
    [SerializeField] private GameObject rerollPanel;
    [SerializeField] private GameObject statChangePanel;

    public void Exit()
    {
        gameObject.SetActive(false);

        mainPanel.SetActive(true);
    }
}
