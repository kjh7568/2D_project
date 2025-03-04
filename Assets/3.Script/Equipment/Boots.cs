using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EquipmentNameSpace;

public class BootsContainer : Equipment
{
    public int equipmentType { get; set; }
    public string name { get; set; }
    public int rarity { get; set; }
    public int baseAttackPower { get; set; }
    public int baseSpellPower { get; set; }
    public List<string> optionDescription { get; set; } = new List<string>();
    public Sprite itemImage { get; set; }
    public float defenseFlat { get; set; }
    public float defensePercent { get; set; }
    public float criticalChance { get; set; }
    public float CriticalDamage { get; set; }
    public float SpellDamage { get; set; }
    public float CastSpeed { get; set; }
    public float Duration { get; set; }
    public float AreaIncrease { get; set; }
    public float maxHpFlat { get; set; }
    public float maxHpPercent { get; set; }
    public float movementSpeedFlat { get; set; }
    public float movementSpeedPercent { get; set; }
    public int[] optionPool { get; set; } = { -1, -1, -1 };
}