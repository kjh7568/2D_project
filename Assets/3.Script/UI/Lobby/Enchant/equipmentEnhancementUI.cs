using EquipmentNameSpace;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class RerollUI : MonoBehaviour
{
    [SerializeField] GameObject previousPanel;

    [SerializeField] Image itemImage;

    [SerializeField] Text rarityText;
    [SerializeField] Text baseStatText;

    [SerializeField] Text[] optionText;

    [SerializeField] Button[] optionCheckButton;
    [SerializeField] Text[] optionButtonText;

    [SerializeField] Button[] inventoryItems;

    [SerializeField] Button optionRerollButton;
    [SerializeField] Text optionRerollButtonText;

    [SerializeField] Button valueRerollButton;
    [SerializeField] Text valueRerollButtonText;

    [SerializeField] Text currentManaFragmentText;


    private List<Action<int>> allOptionPool;

    private enum Option { CriticalChance, CriticalDamage, SpellDamage, CooldownReduction, Duration, AreaIncrease, MaxHpFlat, MaxHpPercent, DefensePercent, movementSpeedPercent }

    private int index;
    private int optionRerollCost;
    private int valueRerollCost;
    private Equipment selectedItem = new WeaponContainer();

    private bool[] isLock;

    private void OnEnable()
    {
        itemImage.gameObject.SetActive(false);

        baseStatText.gameObject.SetActive(false);
        rarityText.gameObject.SetActive(false);

        for (int i = 0; i < 3; i++)
        {
            optionText[i].gameObject.SetActive(false);
            optionCheckButton[i].gameObject.SetActive(false);
        }

        optionRerollButtonText.text = "�ɼ� �����ϱ�";
        optionRerollButton.interactable = false;

        valueRerollButtonText.text = "��ġ �����ϱ�";
        valueRerollButton.interactable = false;

        if (EquipmentManager.instance != null)
        {
            currentManaFragmentText.text = $"���� ����: {EquipmentManager.instance.manaStoneFragment}";
        }

        SetItemImage();
    }
    private void Start()
    {
        allOptionPool = new List<Action<int>>();

        currentManaFragmentText.text = $"���� ����: {EquipmentManager.instance.manaStoneFragment}";

        isLock = new bool[3];
        for (int i = 1; i < 3; i++)
        {
            isLock[i] = false;
        }

        allOptionPool.Add(CriticalChanceOption);
        allOptionPool.Add(CriticalDamageOption);
        allOptionPool.Add(SpellDamageOption);
        allOptionPool.Add(CooldownReductionOption);
        allOptionPool.Add(DurationOption);
        allOptionPool.Add(AreaIncreaseOption);
        allOptionPool.Add(MaxHpFlatOption);
        allOptionPool.Add(MaxHpPercentOption);
        allOptionPool.Add(DefensePercentOption);
        allOptionPool.Add(movementSpeedPercentOption);
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

            switch (selectedItem.rarity)
            {
                case 1:
                    rarityText.color = Color.white;
                    rarityText.text = "���: �Ϲ�";
                    break;
                case 2:
                    rarityText.color = Color.blue;
                    rarityText.text = "���: ����";
                    break;
                case 3:
                    rarityText.color = Color.yellow;
                    rarityText.text = "���: ����";
                    break;
                default:
                    break;
            }
            if (!rarityText.gameObject.activeSelf)
            {
                rarityText.gameObject.SetActive(true);
            }

            switch (selectedItem.equipmentType)
            {
                case 1:
                    baseStatText.text = $"�ֹ���: {selectedItem.baseSpellPower}";
                    break;
                case 2:
                    baseStatText.text = $"����: {selectedItem.defenseFlat}";
                    break;
                case 3:
                    baseStatText.text = $"�̵� �ӵ�: {selectedItem.movementSpeedFlat}";
                    break;
                default:
                    break;
            }
            if (!baseStatText.gameObject.activeSelf)
            {
                baseStatText.gameObject.SetActive(true);
            }

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

            for (int i = 0; i < 3; i++)
            {
                optionButtonText[i].text = "";
                optionText[i].color = Color.white;
                isLock[i] = false;
            }

            int curFragment = EquipmentManager.instance.manaStoneFragment;

            if (curFragment >= optionRerollCost)
            {
                optionRerollButton.interactable = true;
            }
            else
            {
                optionRerollButton.interactable = false;
            }
            if (curFragment >= valueRerollCost)
            {
                valueRerollButton.interactable = true;
            }
            else
            {
                valueRerollButton.interactable = false;
            }

            optionRerollCost = 50;
            valueRerollCost = 80;

            if (selectedItem.rarity == 1)
            {
                optionRerollCost = (int)(optionRerollCost * 0.5);  // 50%
                valueRerollCost = (int)(valueRerollCost * 0.5);
            }
            else if (selectedItem.rarity == 2)
            {
                optionRerollCost = (int)(optionRerollCost * 0.75); // 75%
                valueRerollCost = (int)(valueRerollCost * 0.75);
            }
            else if (selectedItem.rarity == 3)
            {
                optionRerollCost = (int)(optionRerollCost * 1.0);  // 100% (��ȭ ����)
                valueRerollCost = (int)(valueRerollCost * 1.0);
            }

            optionRerollButtonText.text = $"�ɼ� �����ϱ�\n(���� ���� {optionRerollCost}�� �ʿ�)";
            valueRerollButtonText.text = $"��ġ �����ϱ�\n(���� ���� {valueRerollCost}�� �ʿ�)";
        }
        catch
        {
            return;
        }
    }
    public void LockButton(int idx)
    {

        if (isLock[idx])
        {
            isLock[idx] = false;
            optionButtonText[idx].text = "";

            optionText[idx].color = Color.white;
        }
        else
        {
            isLock[idx] = true;
            optionButtonText[idx].text = "V";

            optionText[idx].color = new Color(118f / 255f, 118f / 255f, 118f / 255f);
        }

        if (CheckAllLock())
        {
            optionRerollButtonText.text = "�ɼ� �����ϱ�";
            valueRerollButtonText.text = "��ġ �����ϱ�";
        }
        else
        {
            SetCostManaFragment();
        }
    }
    public bool CheckAllLock()
    {
        bool isAllLock = true;

        for (int i = 0; i < selectedItem.optionDescription.Count; i++)
        {
            if (!isLock[i])
            {
                isAllLock = false;
            }
        }

        if (isAllLock)
        {
            optionRerollButton.interactable = false;
            valueRerollButton.interactable = false;

            return true;
        }
        else
        {
            optionRerollButton.interactable = true;
            valueRerollButton.interactable = true;

            return false;
        }
    }
    public void SetCostManaFragment()
    {
        int lockCount = 0;

        for (int i = 0; i < 3; i++)
        {
            if (isLock[i])
            {
                lockCount++;
            }
        }

        switch (lockCount)
        {
            case 0:
                optionRerollCost = 50;
                valueRerollCost = 80;
                break;
            case 1:
                optionRerollCost = 80;
                valueRerollCost = 100;
                break;
            case 2:
                optionRerollCost = 100;
                valueRerollCost = 150;
                break;
            default:
                break;
        }

        if (selectedItem.rarity == 1)
        {
            optionRerollCost = (int)(optionRerollCost * 0.5);
            valueRerollCost = (int)(valueRerollCost * 0.5);
        }
        else if (selectedItem.rarity == 2)
        {
            optionRerollCost = (int)(optionRerollCost * 0.75);
            valueRerollCost = (int)(valueRerollCost * 0.75);
        }
        else if (selectedItem.rarity == 3)
        {
            optionRerollCost = (int)(optionRerollCost * 1.0);
            valueRerollCost = (int)(valueRerollCost * 1.0);
        }

        optionRerollButtonText.text = $"�ɼ� �����ϱ�\n(���� ���� {optionRerollCost}�� �ʿ�)";
        valueRerollButtonText.text = $"��ġ �����ϱ�\n(���� ���� {valueRerollCost}�� �ʿ�)";

    }
    public void OptionRerollButton()
    {
        int curFragment = EquipmentManager.instance.manaStoneFragment;

        OptionReassignment();

        for (int i = 0; i < selectedItem.optionDescription.Count; i++)
        {
            optionText[i].text = selectedItem.optionDescription[i];
        }

        curFragment -= optionRerollCost;
        currentManaFragmentText.text = $"���� ����: {curFragment}";

        if (curFragment < optionRerollCost)
        {
            optionRerollButton.interactable = false;
        }
        if (curFragment < valueRerollCost)
        {
            valueRerollButton.interactable = false;
        }

        EquipmentManager.instance.manaStoneFragment = curFragment;
    }
    public void RemoveOption(int idx)
    {
        switch (selectedItem.optionPool[idx])
        {
            case (int)Option.CriticalChance:
                selectedItem.criticalChance = 0;
                break;
            case (int)Option.CriticalDamage:
                selectedItem.CriticalDamage = 0;
                break;
            case (int)Option.SpellDamage:
                selectedItem.SpellDamage = 0;
                break;
            case (int)Option.CooldownReduction:
                selectedItem.CastSpeed = 0;
                break;
            case (int)Option.Duration:
                selectedItem.Duration = 0;
                break;
            case (int)Option.AreaIncrease:
                selectedItem.AreaIncrease = 0;
                break;
            case (int)Option.MaxHpFlat:
                selectedItem.maxHpFlat = 0;
                break;
            case (int)Option.MaxHpPercent:
                selectedItem.maxHpPercent = 0;
                break;
            case (int)Option.DefensePercent:
                selectedItem.defensePercent = 0;
                break;
            case (int)Option.movementSpeedPercent:
                selectedItem.movementSpeedPercent = 0;
                break;
            default:
                break;
        }

        selectedItem.optionDescription.RemoveAt(idx);
        selectedItem.optionPool[idx] = -1;
    }
    public void OptionReassignment()
    {
        int[] weaponOptionPool = { 0, 1, 2, 3, 4, 5 };
        int[] armorOptionPool = { 4, 5, 6, 7, 8 };
        int[] bootsOptionPool = { 1, 3, 4, 5, 9 };

        List<int> availableOptions;

        System.Random rand = new System.Random();

        switch (selectedItem.equipmentType)
        {
            case 1:
                for (int i = 0; i < selectedItem.rarity; i++)
                {
                    if (!isLock[i])
                    {
                        RemoveOption(i);

                        availableOptions = weaponOptionPool.Where(x => !selectedItem.optionPool.Contains(x)).ToList();

                        allOptionPool[availableOptions[rand.Next(availableOptions.Count)]](i);
                    }
                }
                break;
            case 2:
                for (int i = 0; i < selectedItem.rarity; i++)
                {
                    if (!isLock[i])
                    {
                        RemoveOption(i);

                        availableOptions = armorOptionPool.Where(x => !selectedItem.optionPool.Contains(x)).ToList();

                        allOptionPool[availableOptions[rand.Next(availableOptions.Count)]](i);
                    }
                }
                break;
            case 3:
                for (int i = 0; i < selectedItem.rarity; i++)
                {
                    if (!isLock[i])
                    {
                        RemoveOption(i);

                        availableOptions = bootsOptionPool.Where(x => !selectedItem.optionPool.Contains(x)).ToList();

                        allOptionPool[availableOptions[rand.Next(availableOptions.Count)]](i);
                    }
                }
                break;
            default:
                break;
        }
    }
    public void ValueRerollButton()
    {
        int curFragment = EquipmentManager.instance.manaStoneFragment;

        for (int i = 0; i < selectedItem.optionDescription.Count; i++)
        {
            if (!isLock[i])
            {
                allOptionPool[selectedItem.optionPool[i]](i);
                selectedItem.optionDescription.RemoveAt(i + 1);
                optionText[i].text = selectedItem.optionDescription[i];
            }
        }

        curFragment -= valueRerollCost;
        currentManaFragmentText.text = $"���� ����: {curFragment}";

        if (curFragment < optionRerollCost)
        {
            optionRerollButton.interactable = false;
        }
        if (curFragment < valueRerollCost)
        {
            valueRerollButton.interactable = false;
        }

        EquipmentManager.instance.manaStoneFragment = curFragment;
    }


    public void CriticalChanceOption(int idx)
    {
        //�Ϲ� ����̸� 1~10
        //���� ����̸� 11~18
        //���� ����̸� 19~24

        selectedItem.optionPool[idx] = 0;

        float value = 0;

        if (selectedItem.rarity == 1)
        {
            value = (float)(UnityEngine.Random.Range(1, 11));
            selectedItem.criticalChance = value;
        }
        else if (selectedItem.rarity == 2)
        {
            value = (float)(UnityEngine.Random.Range(11, 19));
            selectedItem.criticalChance = value;
        }
        else if (selectedItem.rarity == 3)
        {
            value = (float)(UnityEngine.Random.Range(19, 25));
            selectedItem.criticalChance = value;
        }

        selectedItem.optionDescription.Insert(idx, $"ġ��Ÿ Ȯ�� {(int)value}% ����");
    }
    public void CriticalDamageOption(int idx)
    {
        //�Ϲ� ����̸� 1~5
        //���� ����̸� 6~9
        //���� ����̸� 10~12

        selectedItem.optionPool[idx] = 1;

        float value = 0;

        if (selectedItem.rarity == 1)
        {
            value = (float)(UnityEngine.Random.Range(1, 6));
            selectedItem.CriticalDamage = value;
        }
        else if (selectedItem.rarity == 2)
        {
            value = (float)(UnityEngine.Random.Range(6, 10));
            selectedItem.CriticalDamage = value;
        }
        else if (selectedItem.rarity == 3)
        {
            value = (float)(UnityEngine.Random.Range(10, 13));
            selectedItem.CriticalDamage = value;
        }

        selectedItem.optionDescription.Insert(idx, $"ġ��Ÿ ���ط� {(int)value}% ����");
    }
    public void SpellDamageOption(int idx)
    {
        //�Ϲ� ����̸� 10~25
        //���� ����̸� 26~35
        //���� ����̸� 36~40

        selectedItem.optionPool[idx] = 2;

        float value = 0;

        if (selectedItem.rarity == 1)
        {
            value = (float)(UnityEngine.Random.Range(10, 26));
            selectedItem.SpellDamage = value;
        }
        else if (selectedItem.rarity == 2)
        {
            value = (float)(UnityEngine.Random.Range(26, 36));
            selectedItem.SpellDamage = value;
        }
        else if (selectedItem.rarity == 3)
        {
            value = (float)(UnityEngine.Random.Range(36, 41));
            selectedItem.SpellDamage = value;
        }

        selectedItem.optionDescription.Insert(idx, $"�ֹ��� {(int)value}% ����");
    }
    public void CooldownReductionOption(int idx)
    {
        //�Ϲ� ����̸� 1~5
        //���� ����̸� 6~8
        //���� ����̸� 9~10

        selectedItem.optionPool[idx] = 3;

        float value = 0;

        if (selectedItem.rarity == 1)
        {
            value = (float)(UnityEngine.Random.Range(1, 6)); //1~5
            selectedItem.CastSpeed = value;
        }
        else if (selectedItem.rarity == 2)
        {
            value = (float)(UnityEngine.Random.Range(6, 9)); //6~8
            selectedItem.CastSpeed = value;
        }
        else if (selectedItem.rarity == 3)
        {
            value = (float)(UnityEngine.Random.Range(9, 11)); //9~10
            selectedItem.CastSpeed = value;
        }

        selectedItem.optionDescription.Insert(idx, $"��Ÿ�� {(int)value}% ����");
    }
    public void DurationOption(int idx)
    {
        //�Ϲ� ����̸� 1~5
        //���� ����̸� 6~9
        //���� ����̸� 10~15

        selectedItem.optionPool[idx] = 4;

        float value = 0;

        if (selectedItem.rarity == 1)
        {
            value = (float)(UnityEngine.Random.Range(1, 11)); //1~10
            selectedItem.Duration = value;
        }
        else if (selectedItem.rarity == 2)
        {
            value = (float)(UnityEngine.Random.Range(5, 14)); //5~13
            selectedItem.Duration = value;
        }
        else if (selectedItem.rarity == 3)
        {
            value = (float)(UnityEngine.Random.Range(9, 16)); //9~15
            selectedItem.Duration = value;
        }

        selectedItem.optionDescription.Insert(idx, $"��ų ���� �ð� {(int)value}% ����");
    }
    public void AreaIncreaseOption(int idx)
    {
        //�Ϲ� ����̸� 1~7
        //���� ����̸� 8~13
        //���� ����̸� 14~18

        selectedItem.optionPool[idx] = 5;

        float value = 0;

        if (selectedItem.rarity == 1)
        {
            value = (float)(UnityEngine.Random.Range(1, 8)); //1~5
            selectedItem.AreaIncrease = value;
        }
        else if (selectedItem.rarity == 2)
        {
            value = (float)(UnityEngine.Random.Range(8, 14)); //6~8
            selectedItem.AreaIncrease = value;
        }
        else if (selectedItem.rarity == 3)
        {
            value = (float)(UnityEngine.Random.Range(14, 19)); //9~10
            selectedItem.AreaIncrease = value;
        }

        selectedItem.optionDescription.Insert(idx, $"��ų ���� {(int)value}% ����");
    }
    public void MaxHpFlatOption(int idx)
    {
        //�Ϲ� ����̸� 10~14
        //���� ����̸� 15~19
        //���� ����̸� 20~24

        selectedItem.optionPool[idx] = 6;

        float value = 0;

        if (selectedItem.rarity == 1)
        {
            value = (float)(UnityEngine.Random.Range(10, 15));
            selectedItem.maxHpFlat = value;
        }
        else if (selectedItem.rarity == 2)
        {
            value = (float)(UnityEngine.Random.Range(15, 20));
            selectedItem.maxHpFlat = value;
        }
        else if (selectedItem.rarity == 3)
        {
            value = (float)(UnityEngine.Random.Range(20, 25));
            selectedItem.maxHpFlat = value;
        }

        selectedItem.optionDescription.Insert(idx, $"�ִ� ü�� {(int)value} ����");
    }
    public void MaxHpPercentOption(int idx)
    {
        //�Ϲ� ����̸� 5~8
        //���� ����̸� 9~11
        //���� ����̸� 10~13

        selectedItem.optionPool[idx] = 7;

        float value = 0;

        if (selectedItem.rarity == 1)
        {
            value = (float)(UnityEngine.Random.Range(5, 9)); //1~5
            selectedItem.maxHpPercent = value;
        }
        else if (selectedItem.rarity == 2)
        {
            value = (float)(UnityEngine.Random.Range(9, 12)); //6~8
            selectedItem.maxHpPercent = value;
        }
        else if (selectedItem.rarity == 3)
        {
            value = (float)(UnityEngine.Random.Range(10, 14)); //9~10
            selectedItem.maxHpPercent = value;
        }

        selectedItem.optionDescription.Insert(idx, $"������ {(int)value}% ����");
    }
    public void DefensePercentOption(int idx)
    {
        //�Ϲ� ����̸� 15~20
        //���� ����̸� 21~25
        //���� ����̸� 26~28

        selectedItem.optionPool[idx] = 8;

        float value = 0;

        if (selectedItem.rarity == 1)
        {
            value = (float)(UnityEngine.Random.Range(15, 21));
            selectedItem.defensePercent = value;
        }
        else if (selectedItem.rarity == 2)
        {
            value = (float)(UnityEngine.Random.Range(21, 26));
            selectedItem.defensePercent = value;
        }
        else if (selectedItem.rarity == 3)
        {
            value = (float)(UnityEngine.Random.Range(26, 29));
            selectedItem.defensePercent = value;
        }

        selectedItem.optionDescription.Insert(idx, $"���� {(int)value}% ����");
    }
    public void movementSpeedPercentOption(int idx)
    {
        //�Ϲ� ����̸� 10~13
        //���� ����̸� 14~16
        //���� ����̸� 17~20

        selectedItem.optionPool[idx] = 9;

        float value = 0;

        if (selectedItem.rarity == 1)
        {
            value = (float)(UnityEngine.Random.Range(10, 14));
            selectedItem.movementSpeedPercent = value;
        }
        else if (selectedItem.rarity == 2)
        {
            value = (float)(UnityEngine.Random.Range(14, 17));
            selectedItem.movementSpeedPercent = value;
        }
        else if (selectedItem.rarity == 3)
        {
            value = (float)(UnityEngine.Random.Range(17, 21));
            selectedItem.movementSpeedPercent = value;
        }

        selectedItem.optionDescription.Insert(idx, $"�̵� �ӵ� {(int)value}% ����");
    }
}
