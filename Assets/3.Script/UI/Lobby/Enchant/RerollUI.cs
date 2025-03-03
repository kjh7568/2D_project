using EquipmentNameSpace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RerollUI : MonoBehaviour
{
    [SerializeField] GameObject previousPanel;

    [SerializeField] Image itemImage;

    [SerializeField] Text[] optionText;

    [SerializeField] Button[] optionCheckButton;
    [SerializeField] Text[] optionButtonText;

    [SerializeField] Button[] inventoryItems;

    [SerializeField] Button rerollButton;
    [SerializeField] Text rerollButtonText;
    [SerializeField] Text currentManaFragmentText;

    private int index;
    private Equipment selectedItem = new WeaponContainer();

    private void OnEnable()
    {
        itemImage.gameObject.SetActive(false);

        for (int i = 0; i < 3; i++)
        {
            optionText[i].gameObject.SetActive(false);
            optionCheckButton[i].gameObject.SetActive(false);
        }

        rerollButtonText.text = "옵션 변경하기";
        rerollButton.interactable = false;

        if (EquipmentManager.instance != null)
        {
            currentManaFragmentText.text = $"마석 조각: {EquipmentManager.instance.manaStoneFragment}";
        }

        SetItemImage();
    }
    private void Start()
    {
        currentManaFragmentText.text = $"마석 조각: {EquipmentManager.instance.manaStoneFragment}";
    }
    public void ExitButton()
    {
        previousPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void SetItemImage()
    {
        List<Equipment> inventory = EquipmentManager.instance.inventory;

        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventoryItems[i].TryGetComponent(out Image image))
            {
                image.sprite = inventory[i].itemImage;
            }
        }
    }
    public void SelectEquipment(int idx)
    {
        index = idx;

        try
        {
            selectedItem = EquipmentManager.instance.inventory[idx];

            if (!itemImage.gameObject.activeSelf)
            {
                itemImage.gameObject.SetActive(true);
            }
            itemImage.sprite = selectedItem.itemImage;

            for (int i = 0; i < selectedItem.optionDescription.Count; i++)
            {
                if (!optionText[i].gameObject.activeSelf)
                {
                    optionText[i].gameObject.SetActive(true);
                    optionCheckButton[i].gameObject.SetActive(true);
                }

                optionText[i].text = selectedItem.optionDescription[i];
                optionButtonText[i].text = "";
            }
            for (int i = selectedItem.optionDescription.Count; i < 3; i++)
            {
                if (optionText[i].gameObject.activeSelf)
                {
                    optionText[i].gameObject.SetActive(false);
                    optionCheckButton[i].gameObject.SetActive(false);
                }
            }


            //equipButton.interactable = true;
            //shatterButton.interactable = true;

            //PreviewImage.sprite = selectedItem.itemImage;
            //SetText();
        }
        catch
        {
            return;
        }
    }
}
