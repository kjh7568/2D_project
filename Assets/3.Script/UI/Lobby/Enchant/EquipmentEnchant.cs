using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentEnchant : MonoBehaviour
{
    [SerializeField] private GameObject PreviousPanel;
    [SerializeField] private GameObject optionRerollPanel;
    [SerializeField] private GameObject optionValueChangePanel;

    public void ExitButton()
    {
        gameObject.SetActive(false);

        PreviousPanel.SetActive(true);
    }

    public void OptionRerollButton()
    {
        gameObject.SetActive(false);

        optionRerollPanel.SetActive(true);
    }

    public void OptionValueChangeButton()
    {
        gameObject.SetActive(false);

        optionValueChangePanel.SetActive(true);
    }
}
