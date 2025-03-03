using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using EquipmentNameSpace;

public class LobbyItemGachaNotificationPanel : MonoBehaviour
{
    [SerializeField] private Button[] buttons;

    [SerializeField] private Text itemName;
    [SerializeField] private Text itemRarity;
    [SerializeField] private Text itemStats;

    [SerializeField] private Image itemImage;

    [SerializeField] private Sprite[] wandImages;
    [SerializeField] private Sprite[] armorImages;
    [SerializeField] private Sprite[] bootsImages;

    private string[] wandName = { "표류목 지팡이", "조각된 지팡이", "나선형 지팡이" };
    private Tuple<int, int>[] baseSpellPowerRandomRange = { new Tuple<int, int>(5, 10),
                                                                       new Tuple<int, int>(10, 15),
                                                                       new Tuple<int, int>(15, 20) };

    private string[] armorName = { "천 갑옷", "가죽 갑옷", "판금 갑옷" };
    private Tuple<int, int>[] defenseFlatRandomRange = { new Tuple<int, int>(1, 2),
                                                                       new Tuple<int, int>(1, 3),
                                                                       new Tuple<int, int>(2, 4) };

    private string[] bootsName = { "천 장화", "가죽 장화", "판금 장화" };
    private Tuple<int, int>[] movementSpeedFlatRandomRange = { new Tuple<int, int>(3, 4),
                                                                       new Tuple<int, int>(3, 5),
                                                                       new Tuple<int, int>(4, 6) };

    private List<Action<Equipment>> wandOptionPool;
    private List<Action<Equipment>> armorOptionPool;
    private List<Action<Equipment>> bootsOptionPool;

    private int[] randomOption;

    private Equipment newItem;

    private void Awake()
    {
        wandOptionPool = new List<Action<Equipment>>();

        wandOptionPool.Add(CriticalChanceOption);
        wandOptionPool.Add(CriticalDamageOption);
        wandOptionPool.Add(SpellDamageOption);
        wandOptionPool.Add(CooldownReductionOption);
        wandOptionPool.Add(DurationOption);
        wandOptionPool.Add(AreaIncreaseOption);

        armorOptionPool = new List<Action<Equipment>>();

        armorOptionPool.Add(DurationOption);
        armorOptionPool.Add(AreaIncreaseOption);
        armorOptionPool.Add(MaxHpFlatOption);
        armorOptionPool.Add(MaxHpPercentOption);
        armorOptionPool.Add(DefensePercentOption);

        bootsOptionPool = new List<Action<Equipment>>();

        bootsOptionPool.Add(CriticalDamageOption);
        bootsOptionPool.Add(movementSpeedPercentOption);
        bootsOptionPool.Add(CooldownReductionOption);
        bootsOptionPool.Add(DurationOption);
        bootsOptionPool.Add(AreaIncreaseOption);
    }

    private void OnEnable()
    {
        //여기서 랜덤 장비 종류, 등급, 능력치 등을 정한다
        switch (EquipmentManager.instance.equipmentType)
        {
            case 1:
                randomOption = Enumerable.Range(0, 6).OrderBy(_ => UnityEngine.Random.value).Take(3).ToArray();
                newItem = RandomWeapon();
                newItem.equipmentType = 1;
                SetText();
                EquipmentManager.instance.inventory.Add(newItem);
                break;
            case 2:
                randomOption = Enumerable.Range(0, 5).OrderBy(_ => UnityEngine.Random.value).Take(3).ToArray();
                newItem = RandomArmor();
                newItem.equipmentType = 2;
                SetText();
                EquipmentManager.instance.inventory.Add(newItem);
                break;
            case 3:
                randomOption = Enumerable.Range(0, 4).OrderBy(_ => UnityEngine.Random.value).Take(3).ToArray();
                newItem = RandomBoots();
                newItem.equipmentType = 3;
                SetText();
                EquipmentManager.instance.inventory.Add(newItem);
                break;
            default:
                break;
        }
    }
    public void ConfirmButton()
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = true;
        }

        gameObject.SetActive(false);
    }

    public void SetText()
    {
        itemName.text = newItem.name;

        if (newItem.rarity == 1)
        {
            itemRarity.text = "일반";
            itemRarity.color = Color.white;
        }
        else if (newItem.rarity == 2)
        {
            itemRarity.text = "매직";
            itemRarity.color = Color.blue;
        }
        else if (newItem.rarity == 3)
        {
            itemRarity.text = "레어";
            itemRarity.color = Color.yellow;
        }

        if (newItem.equipmentType == 1)
        {
            string str = $"주문력: {newItem.baseSpellPower}\n\n";
            for (int i = 0; i < newItem.optionDescription.Count; i++)
            {
                str += newItem.optionDescription[i];

                if (i - 1 != newItem.rarity)
                {
                    str += "\n\n";
                }
            }
            itemStats.text = str;
        }
        else if (newItem.equipmentType == 2)
        {
            string str = $"방어력: {newItem.defenseFlat}\n\n";
            for (int i = 0; i < newItem.optionDescription.Count; i++)
            {
                str += newItem.optionDescription[i];

                if (i - 1 != newItem.rarity)
                {
                    str += "\n\n";
                }
            }
            itemStats.text = str;
        }
        else if (newItem.equipmentType == 3)
        {
            string str = $"이동 속도: {newItem.movementSpeedFlat}\n\n";
            for (int i = 0; i < newItem.optionDescription.Count; i++)
            {
                str += newItem.optionDescription[i];

                if (i - 1 != newItem.rarity)
                {
                    str += "\n\n";
                }
            }
            itemStats.text = str;
        }
    }

    public WeaponContainer RandomWeapon()
    {
        newItem = new WeaponContainer();

        int rate, idx = 0;

        rate = UnityEngine.Random.Range(0, 100);

        if (rate < 65)
        {
            idx = 0;
        }
        else if (rate < 90)
        {
            idx = 1;
        }
        else if (rate < 100)
        {
            idx = 2;
        }

        itemImage.sprite = wandImages[idx];
        newItem.itemImage = wandImages[idx];
        newItem.name = wandName[idx];
        newItem.baseSpellPower = UnityEngine.Random.Range(baseSpellPowerRandomRange[idx].Item1, baseSpellPowerRandomRange[idx].Item2);

        rate = UnityEngine.Random.Range(0, 100);

        if (rate < 65)
        {
            newItem.rarity = 1;
        }
        else if (rate < 90)
        {
            newItem.rarity = 2;
        }
        else if (rate < 100)
        {
            newItem.rarity = 3;
        }

        for (int i = 0; i < newItem.rarity; i++)
        {
            wandOptionPool[randomOption[i]](newItem);
        }

        return (WeaponContainer)newItem;
    }
    public ArmorContainer RandomArmor()
    {
        newItem = new ArmorContainer();

        int rate, idx = 0;

        rate = UnityEngine.Random.Range(0, 100);

        if (rate < 65)
        {
            idx = 0;
        }
        else if (rate < 90)
        {
            idx = 1;
        }
        else if (rate < 100)
        {
            idx = 2;
        }

        itemImage.sprite = armorImages[idx];
        newItem.itemImage = armorImages[idx];
        newItem.name = armorName[idx];
        newItem.defenseFlat = UnityEngine.Random.Range(defenseFlatRandomRange[idx].Item1, defenseFlatRandomRange[idx].Item2);

        rate = UnityEngine.Random.Range(0, 100);

        if (rate < 65)
        {
            newItem.rarity = 1;
        }
        else if (rate < 90)
        {
            newItem.rarity = 2;
        }
        else if (rate < 100)
        {
            newItem.rarity = 3;
        }

        for (int i = 0; i < newItem.rarity; i++)
        {
            armorOptionPool[randomOption[i]](newItem);
        }

        return (ArmorContainer)newItem;
    }
    public BootsContainer RandomBoots()
    {
        newItem = new BootsContainer();

        int rate, idx = 0;

        rate = UnityEngine.Random.Range(0, 100);

        if (rate < 65)
        {
            idx = 0;
        }
        else if (rate < 90)
        {
            idx = 1;
        }
        else if (rate < 100)
        {
            idx = 2;
        }

        itemImage.sprite = bootsImages[idx];
        newItem.itemImage = bootsImages[idx];
        newItem.name = bootsName[idx];
        newItem.movementSpeedFlat = UnityEngine.Random.Range(movementSpeedFlatRandomRange[idx].Item1, movementSpeedFlatRandomRange[idx].Item2);

        rate = UnityEngine.Random.Range(0, 100);

        if (rate < 65)
        {
            newItem.rarity = 1;
        }
        else if (rate < 90)
        {
            newItem.rarity = 2;
        }
        else if (rate < 100)
        {
            newItem.rarity = 3;
        }

        for (int i = 0; i < newItem.rarity; i++)
        {
            rate = UnityEngine.Random.Range(0, 100);

            if (rate > 20)
            {
                bootsOptionPool[randomOption[i]](newItem);
            }
        }

        return (BootsContainer)newItem;
    }



    public void CriticalChanceOption(Equipment newItem)
    {
        //일반 등급이면 1~10
        //매직 등급이면 11~18
        //레어 등급이면 19~24

        float value = 0;

        if (newItem.rarity == 1)
        {
            value = (float)(UnityEngine.Random.Range(1, 11));
            newItem.criticalChance = value;
        }
        else if (newItem.rarity == 2)
        {
            value = (float)(UnityEngine.Random.Range(11, 19));
            newItem.criticalChance = value;
        }
        else if (newItem.rarity == 3)
        {
            value = (float)(UnityEngine.Random.Range(19, 25));
            newItem.criticalChance = value;
        }

        newItem.optionDescription.Add($"치명타 확률 {(int)value}% 증가");
    }
    public void CriticalDamageOption(Equipment newItem)
    {
        //일반 등급이면 1~5
        //매직 등급이면 6~9
        //레어 등급이면 10~12

        float value = 0;

        if (newItem.rarity == 1)
        {
            value = (float)(UnityEngine.Random.Range(1, 6));
            newItem.CriticalDamage = value;
        }
        else if (newItem.rarity == 2)
        {
            value = (float)(UnityEngine.Random.Range(6, 10));
            newItem.CriticalDamage = value;
        }
        else if (newItem.rarity == 3)
        {
            value = (float)(UnityEngine.Random.Range(10, 13));
            newItem.CriticalDamage = value;
        }

        newItem.optionDescription.Add($"치명타 피해량 {(int)value}% 증가");
    }
    public void SpellDamageOption(Equipment newItem)
    {
        //일반 등급이면 10~25
        //매직 등급이면 26~35
        //레어 등급이면 36~40

        float value = 0;

        if (newItem.rarity == 1)
        {
            value = (float)(UnityEngine.Random.Range(10, 26));
            newItem.SpellDamage = value;
        }
        else if (newItem.rarity == 2)
        {
            value = (float)(UnityEngine.Random.Range(26, 36));
            newItem.SpellDamage = value;
        }
        else if (newItem.rarity == 3)
        {
            value = (float)(UnityEngine.Random.Range(36, 41));
            newItem.SpellDamage = value;
        }

        newItem.optionDescription.Add($"주문력 {(int)value}% 증가");
    }
    public void CooldownReductionOption(Equipment newItem)
    {
        //일반 등급이면 1~5
        //매직 등급이면 6~8
        //레어 등급이면 9~10

        float value = 0;

        if (newItem.rarity == 1)
        {
            value = (float)(UnityEngine.Random.Range(1, 6)); //1~5
            newItem.CastSpeed = value;
        }
        else if (newItem.rarity == 2)
        {
            value = (float)(UnityEngine.Random.Range(6, 9)); //6~8
            newItem.CastSpeed = value;
        }
        else if (newItem.rarity == 3)
        {
            value = (float)(UnityEngine.Random.Range(9, 11)); //9~10
            newItem.CastSpeed = value;
        }

        newItem.optionDescription.Add($"쿨타임 {(int)value}% 감소");
    }
    public void DurationOption(Equipment newItem)
    {
        //일반 등급이면 1~5
        //매직 등급이면 6~9
        //레어 등급이면 10~15

        float value = 0;

        if (newItem.rarity == 1)
        {
            value = (float)(UnityEngine.Random.Range(1, 11)); //1~10
            newItem.Duration = value;
        }
        else if (newItem.rarity == 2)
        {
            value = (float)(UnityEngine.Random.Range(5, 14)); //5~13
            newItem.Duration = value;
        }
        else if (newItem.rarity == 3)
        {
            value = (float)(UnityEngine.Random.Range(9, 16)); //9~15
            newItem.Duration = value;
        }

        newItem.optionDescription.Add($"스킬 지속 시간 {(int)value}% 증가");
    }
    public void AreaIncreaseOption(Equipment newItem)
    {
        //일반 등급이면 1~7
        //매직 등급이면 8~13
        //레어 등급이면 14~18

        float value = 0;

        if (newItem.rarity == 1)
        {
            value = (float)(UnityEngine.Random.Range(1, 8)); //1~5
            newItem.AreaIncrease = value;
        }
        else if (newItem.rarity == 2)
        {
            value = (float)(UnityEngine.Random.Range(8, 14)); //6~8
            newItem.AreaIncrease = value;
        }
        else if (newItem.rarity == 3)
        {
            value = (float)(UnityEngine.Random.Range(14, 19)); //9~10
            newItem.AreaIncrease = value;
        }

        newItem.optionDescription.Add($"스킬 범위 {(int)value}% 증가");
    }
    public void MaxHpFlatOption(Equipment newItem)
    {
        //일반 등급이면 10~14
        //매직 등급이면 15~19
        //레어 등급이면 20~24

        float value = 0;

        if (newItem.rarity == 1)
        {
            value = (float)(UnityEngine.Random.Range(10, 15));
            newItem.maxHpFlat = value;
        }
        else if (newItem.rarity == 2)
        {
            value = (float)(UnityEngine.Random.Range(15, 20));
            newItem.maxHpFlat = value;
        }
        else if (newItem.rarity == 3)
        {
            value = (float)(UnityEngine.Random.Range(20, 25));
            newItem.maxHpFlat = value;
        }

        newItem.optionDescription.Add($"최대 체력 {(int)value} 증가");
    }
    public void MaxHpPercentOption(Equipment newItem)
    {
        //일반 등급이면 5~8
        //매직 등급이면 9~11
        //레어 등급이면 10~13

        float value = 0;

        if (newItem.rarity == 1)
        {
            value = (float)(UnityEngine.Random.Range(5, 9)); //1~5
            newItem.maxHpPercent = value;
        }
        else if (newItem.rarity == 2)
        {
            value = (float)(UnityEngine.Random.Range(9, 12)); //6~8
            newItem.maxHpPercent = value;
        }
        else if (newItem.rarity == 3)
        {
            value = (float)(UnityEngine.Random.Range(10, 14)); //9~10
            newItem.maxHpPercent = value;
        }

        newItem.optionDescription.Add($"강인함 {(int)value}% 증가");
    }
    public void DefensePercentOption(Equipment newItem)
    {
        //일반 등급이면 15~20
        //매직 등급이면 21~25
        //레어 등급이면 26~28

        float value = 0;

        if (newItem.rarity == 1)
        {
            value = (float)(UnityEngine.Random.Range(15, 21));
            newItem.defensePercent = value;
        }
        else if (newItem.rarity == 2)
        {
            value = (float)(UnityEngine.Random.Range(21, 26));
            newItem.defensePercent = value;
        }
        else if (newItem.rarity == 3)
        {
            value = (float)(UnityEngine.Random.Range(26, 29));
            newItem.defensePercent = value;
        }

        newItem.optionDescription.Add($"방어력 {(int)value}% 증가");
    }
    public void movementSpeedPercentOption(Equipment newItem)
    {
        //일반 등급이면 10~13
        //매직 등급이면 14~16
        //레어 등급이면 17~20

        float value = 0;

        if (newItem.rarity == 1)
        {
            value = (float)(UnityEngine.Random.Range(10, 14));
            newItem.movementSpeedPercent = value;
        }
        else if (newItem.rarity == 2)
        {
            value = (float)(UnityEngine.Random.Range(14, 17));
            newItem.movementSpeedPercent = value;
        }
        else if (newItem.rarity == 3)
        {
            value = (float)(UnityEngine.Random.Range(17, 21));
            newItem.movementSpeedPercent = value;
        }

        newItem.optionDescription.Add($"이동 속도 {(int)value}% 증가");
    }
}
