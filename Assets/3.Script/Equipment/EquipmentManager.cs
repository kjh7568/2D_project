using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EquipmentNameSpace;

namespace EquipmentNameSpace
{
    public interface Equipment
    {
        public int equipmentType { get; set; }
        public string name { get; set; }
        public int rarity { get; set; }
        public int baseAttackPower { get; set; }
        public int baseSpellPower { get; set; }
        public List<string> optionDescription { get; set; }
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

    }
}

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance { get; private set; }

    public int equipmentType = 0;

    public List<Equipment> inventory = new List<Equipment>();

    public int manaStoneFragment;

    public WeaponContainer EquippedWeapon;
    public ArmorContainer EquippedArmor;
    public BootsContainer EquippedBoots;
    //public BootsContainer EquippedBoots;

    private void Awake()
    {
        //디버그용
        manaStoneFragment = 3000;
        //manaStoneFragment = 0;

        EquippedBoots = null;
        EquippedArmor = null;
        EquippedWeapon = null;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}