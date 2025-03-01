using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EquipmentNameSpace;

public class LobbyInventoryUI : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private Button[] itemButtons;

    [SerializeField] private Image PreviewImage;

    [SerializeField] private Text itemName;
    [SerializeField] private Text itemRarity;
    [SerializeField] private Text itemStats;
    [SerializeField] private Text ManaStoneFragment;

    [SerializeField] private Button equipButton;
    [SerializeField] private Button shatterButton;

    private Equipment selectedItem = new WeaponContainer();

    private int index;

    private void OnEnable()
    {
        ManaStoneFragment.text = $"마석 조각: {EquipmentManager.instance.manaStoneFragment}개";

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = false;
        }

        PreviewImage.gameObject.SetActive(false);
        itemName.gameObject.SetActive(false);
        itemRarity.gameObject.SetActive(false);
        itemStats.gameObject.SetActive(false);

        equipButton.interactable = false;
        shatterButton.interactable = false;

        List<Equipment> inventory = EquipmentManager.instance.inventory;
        for (int i = 0; i < inventory.Count; i++)
        {
            if(itemButtons[i].TryGetComponent(out Image image))
            {
                image.sprite = inventory[i].itemImage;
            }
        }
    }
    public void ExitButton()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }

        gameObject.SetActive(false);
    }
    public void SelectEquipment(int idx)
    {
        index = idx;

        try
        {
            selectedItem = EquipmentManager.instance.inventory[idx];

            PreviewImage.gameObject.SetActive(true);
            itemName.gameObject.SetActive(true);
            itemRarity.gameObject.SetActive(true);
            itemStats.gameObject.SetActive(true);

            equipButton.interactable = true;
            shatterButton.interactable = true;

            PreviewImage.sprite = selectedItem.itemImage;
            SetText();
        }
        catch
        {
            return;
        }
    }

    private void Update()
    {
        if(selectedItem == EquipmentManager.instance.EquippedWeapon)
        {
            equipButton.interactable = false;
        }
        else if(selectedItem == EquipmentManager.instance.EquippedArmor)
        {
            equipButton.interactable = false;
        }
        else if (selectedItem == EquipmentManager.instance.EquippedBoots)
        {
            equipButton.interactable = false;
        }
    }

    public void SetText()
    {
        itemName.text = selectedItem.name;

        if (selectedItem.rarity == 1)
        {
            itemRarity.text = "일반";
            itemRarity.color = Color.white;
        }
        else if (selectedItem.rarity == 2)
        {
            itemRarity.text = "매직";
            itemRarity.color = Color.blue;
        }
        else if (selectedItem.rarity == 3)
        {
            itemRarity.text = "레어";
            itemRarity.color = Color.yellow;
        }

        if (selectedItem.equipmentType == 1)
        {
            string str = $"주문력: {selectedItem.baseSpellPower}\n\n";
            for (int i = 0; i < selectedItem.optionDescription.Count; i++)
            {
                str += selectedItem.optionDescription[i];

                if (i - 1 != selectedItem.rarity)
                {
                    str += "\n\n";
                }
            }
            itemStats.text = str;
        }
        else if (selectedItem.equipmentType == 2)
        {
            string str = $"방어력: {selectedItem.defenseFlat}\n\n";
            for (int i = 0; i < selectedItem.optionDescription.Count; i++)
            {
                str += selectedItem.optionDescription[i];

                if (i - 1 != selectedItem.rarity)
                {
                    str += "\n\n";
                }
            }
            itemStats.text = str;
        }
        else if (selectedItem.equipmentType == 3)
        {
            string str = $"이동 속도: {selectedItem.movementSpeedFlat}\n\n";
            for (int i = 0; i < selectedItem.optionDescription.Count; i++)
            {
                str += selectedItem.optionDescription[i];

                if (i - 1 != selectedItem.rarity)
                {
                    str += "\n\n";
                }
            }
            itemStats.text = str;
        }
    }
    public void EquipButton()
    {   
        if (selectedItem.equipmentType == 1)
        {
            EquipmentManager.instance.EquippedWeapon = (WeaponContainer)selectedItem;
        }
        else if (selectedItem.equipmentType == 2)
        {
            EquipmentManager.instance.EquippedArmor = (ArmorContainer)selectedItem;
        }
        else if (selectedItem.equipmentType == 3)
        {
            EquipmentManager.instance.EquippedBoots = (BootsContainer)selectedItem;
        }
    }
    public void ShatterButton()
    {
        switch (selectedItem.rarity)
        {
            case 1:
                EquipmentManager.instance.manaStoneFragment += 5;
                break;

            case 2:
                EquipmentManager.instance.manaStoneFragment += 10;
                break;

            case 3:
                EquipmentManager.instance.manaStoneFragment += 20;
                break;

        }

        EquipmentManager.instance.inventory.RemoveAt(index);
    }
}
