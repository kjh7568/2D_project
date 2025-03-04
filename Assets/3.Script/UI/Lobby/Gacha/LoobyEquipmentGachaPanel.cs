using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoobyEquipmentGachaPanel : MonoBehaviour
{
    [SerializeField] private GameObject equipmentPanel;
    [SerializeField] private GameObject ItemGachaNotificationPanel;

    [SerializeField] private Image PreviewImage;

    [SerializeField] private Button[] buttons;

    [SerializeField] private Sprite[] equipmentImage;

    [SerializeField] private Text ManaStoneFragment;

    private void OnEnable()
    {
        ManaStoneFragment.text = $"마석 조각: {EquipmentManager.instance.manaStoneFragment}개";

        buttons[4].interactable = false;
    }

    private void OnDisable()
    {
        PreviewImage.gameObject.SetActive(false);
    }

    public void Exit()
    {
        EquipmentManager.instance.equipmentType = 0;

        gameObject.SetActive(false);

        equipmentPanel.SetActive(true);
    }
    public void WeaponGachaButton()
    {
        EquipmentManager.instance.equipmentType = 1;
        PreviewImage.gameObject.SetActive(true);
        PreviewImage.sprite = equipmentImage[0];

        if (EquipmentManager.instance.manaStoneFragment >= 30)
        {
            buttons[4].interactable = true;
        }
    }
    public void ArmorGachaButton()
    {
        EquipmentManager.instance.equipmentType = 2;
        PreviewImage.gameObject.SetActive(true);
        PreviewImage.sprite = equipmentImage[1];

        if (EquipmentManager.instance.manaStoneFragment >= 30)
        {
            buttons[4].interactable = true;
        }
    }
    public void BootsGachaButton()
    {
        EquipmentManager.instance.equipmentType = 3;
        PreviewImage.gameObject.SetActive(true);
        PreviewImage.sprite = equipmentImage[2];

        if (EquipmentManager.instance.manaStoneFragment >= 30)
        {
            buttons[4].interactable = true;
        }
    }
    public void GachaButton()
    {
        if (EquipmentManager.instance.manaStoneFragment >= 30 && EquipmentManager.instance.inventory.Count < 30)
        {
            if (EquipmentManager.instance.equipmentType != 0)
            {
                for (int i = 0; i < buttons.Length; i++)
                {
                    buttons[i].interactable = false;
                }

                ItemGachaNotificationPanel.SetActive(true);
            }

            EquipmentManager.instance.manaStoneFragment -= 30;
            ManaStoneFragment.text = $"마석 조각: {EquipmentManager.instance.manaStoneFragment}개";
        }

        if (EquipmentManager.instance.manaStoneFragment < 30)
        {
            buttons[4].interactable = false;
        }
    }
}
