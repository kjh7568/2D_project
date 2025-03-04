using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    public float movementSpeed { get; private set; }
    public float movementSpeedFlat;
    public float movementSpeedPercent;


    public float hp { get; private set; }
    public float maxHp { get; private set; }
    public float maxHpFlat;
    public float maxHpPercent;


    public float defense { get; private set; }
    public float defenseFlat;
    public float defensePercent;


    public int level { get; private set; }
    public float exp { get; private set; }
    public float maxExp { get; private set; }
    public float setMaxExp;


    public float attackPower { get; private set; }
    public float attackPowerFlat;
    public float attackPowerPercent;


    public float spellPower { get; private set; }
    public float spellPowerFlat;
    public float spellPowerPercent;


    public float criticalChance;
    public float damageTakenAmount;

    public float skillExpansion;
    public float durationIncrease;
    public float cooldownReduction;

    public bool isGetReward;
    public bool isLevelUp;


    public void Awake()
    {
        AddItemState();

        UpdateMovementSpeed();
        UpdateMaxHp();
        UpdateSpellPower();
        UpdateAttackPower();
        UpdateDefense();

        hp = maxHp;

        level = 1;
        maxExp = setMaxExp;
        //����� ��
        exp = 18;
        //exp = 0;

        isGetReward = false;
        isLevelUp = false;

        Debug.Log($"�ִ� ü��: {maxHp}");
        Debug.Log($"����: {defense}");
        Debug.Log($"���ݷ�: {attackPower}");
        Debug.Log($"�ֹ���: {spellPower}");
        Debug.Log($"�̵� �ӵ�: {movementSpeed}");
        Debug.Log($"ġȮ/ġ��: {criticalChance}/{damageTakenAmount}");
        Debug.Log($"��ų ����: {skillExpansion}");
        Debug.Log($"��ų ���� �ð�: {durationIncrease}");
        Debug.Log($"��ų ��Ÿ�� ����: {cooldownReduction}");
    }

    public void setExp(float exp)
    {
        this.exp += exp;

        if (this.exp >= maxExp)
        {
            isLevelUp = true;
        }
    }

    public void setHp(float hp)
    {
        this.hp += hp;

        if (this.hp > maxHp)
        {
            this.hp = maxHp;
        }
    }

    public void SetMaxExp(float maxExp)
    {
        this.maxExp += maxExp;
    }

    public void LevelUp()
    {
        this.level++;
    }
    public void AddItemState()
    {
        WeaponContainer weapon = EquipmentManager.instance.EquippedWeapon;
        ArmorContainer armor = EquipmentManager.instance.EquippedArmor;
        BootsContainer boots = EquipmentManager.instance.EquippedBoots;

        spellPowerFlat += weapon.baseSpellPower;
        criticalChance += weapon.criticalChance;
        damageTakenAmount += (weapon.CriticalDamage + boots.CriticalDamage) / 100f;
        spellPowerPercent += weapon.SpellDamage / 100f;
        cooldownReduction += (weapon.CastSpeed + boots.CastSpeed) / 100f;
        durationIncrease += (weapon.Duration + armor.Duration + boots.Duration) / 100f;
        skillExpansion += (weapon.AreaIncrease + armor.AreaIncrease + boots.AreaIncrease) / 100f;

        defenseFlat += armor.defenseFlat;
        defensePercent += armor.defensePercent / 100f;
        maxHpFlat += armor.maxHpFlat;
        maxHpPercent += armor.maxHpPercent / 100f;

        movementSpeedFlat += boots.movementSpeedFlat;
        movementSpeedPercent += boots.movementSpeedPercent / 100f;
    }



    public void UpdateMaxHp()
    {
        maxHp = maxHpFlat * maxHpPercent;
    }
    public void UpdateSpellPower()
    {
        spellPower = spellPowerFlat * spellPowerPercent;
    }
    public void UpdateAttackPower()
    {
        attackPower = attackPowerFlat * attackPowerPercent;
    }
    public void UpdateMovementSpeed()
    {
        movementSpeed = movementSpeedFlat * movementSpeedPercent;
    }
    public void UpdateDefense()
    {
        defense = defenseFlat * defensePercent;
    }
}
