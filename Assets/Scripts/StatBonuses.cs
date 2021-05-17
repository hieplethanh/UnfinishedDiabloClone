using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseStat
{
    public EquipmentAffixClass affix { get; set; }
    public float value { get; set; }

    public BaseStat(EquipmentAffixClass aff, float val)
    {
        affix = aff;
        value = val;
    }

    public static BaseStat GetRandomizeStat(EquipmentAffixClass minAff, EquipmentAffixClass maxAff, float minVal, float maxVal)
    {
        EquipmentAffixClass affix = (EquipmentAffixClass)Random.Range((int)minAff, (int)maxAff);
        float value = Random.Range(minVal, maxVal); //need to randomize based on level
        return new BaseStat(affix, value);
    }

    public static BaseStat GetRandomizeStat(EquipmentAffixClass affix, float minVal, float maxVal)
    {
        float value = Random.Range(minVal, maxVal);
        return new BaseStat(affix, value);
    }

    public static BaseStat GetRandomizeStat(EquipmentAffixClass minAff, EquipmentAffixClass maxAff, int itemLevel)
    {
        EquipmentAffixClass affix = (EquipmentAffixClass)Random.Range((int)minAff, (int)maxAff);
        float value = 0.0f;
        switch (affix)
        {
            case EquipmentAffixClass.WeaponDamage:
                value = Random.Range(5 + itemLevel * 2, 10 + itemLevel * 2);
                break;

            case EquipmentAffixClass.AttackSpeed:
                value = 0;//Random.Range(5 + itemLevel * 2, 10 + itemLevel * 2);
                break;

            case EquipmentAffixClass.BonusDamagePercent:
                value = Random.Range(2, 15);
                break;

            case EquipmentAffixClass.Armor:
                value = Random.Range(itemLevel * 0.5f + 2, itemLevel + 5);
                break;

            case EquipmentAffixClass.BonusHealth:
                value = Random.Range(itemLevel * 2 + 10, itemLevel * 2.5f + 20);
                break;

            case EquipmentAffixClass.DamageReduction:
                value = Random.Range(2, 7);
                break;

            case EquipmentAffixClass.FireResistance:
                value = Random.Range(5 + itemLevel * 2, 10 + itemLevel * 2);
                break;

            case EquipmentAffixClass.ColdResistance:
                value = Random.Range(5 + itemLevel * 2, 10 + itemLevel * 2);
                break;

            case EquipmentAffixClass.LightningResistance:
                value = Random.Range(5 + itemLevel * 2, 10 + itemLevel * 2);
                break;

            case EquipmentAffixClass.MagicResistance:
                value = Random.Range(5 + itemLevel * 2, 10 + itemLevel * 2);
                break;

            case EquipmentAffixClass.PoisonResistance:
                value = Random.Range(5 + itemLevel * 2, 10 + itemLevel * 2);
                break;

            case EquipmentAffixClass.BonusStrength:
                value = Random.Range(2 + itemLevel, 5 + itemLevel * 2);
                break;

            case EquipmentAffixClass.BonusVitality:
                value = Random.Range(2 + itemLevel, 5 + itemLevel * 2);
                break;

            case EquipmentAffixClass.BonusAgility:
                value = Random.Range(2 + itemLevel, 5 + itemLevel * 2);
                break;

            case EquipmentAffixClass.BonusIntelligent:
                value = Random.Range(2 + itemLevel, 5 + itemLevel * 2);
                break;
        }
        
        return new BaseStat(affix, value);
    }

    public static string[] StatDescriptions = new string[]
    {
        "Weapon Damage", // Basic affixes 0->2
        "Attack Speed",
        "Damage %",

        "Armor", // armors 3->5
        "Health",
        "Damage Reduction %",

        "Fire Resistance", // resist 6->10
        "Cold Resistance",
        "Lightning Resistance",
        "Magic Resistance",
        "Poison Resistance",

        "Strength", // common 11->14
        "Vitality",
        "Agility",
        "Intelligent"
    };
}

public class StatBonuses
{
    public List<BaseStat> Bonuses { get; set; }

    public StatBonuses()
    {
        Bonuses = new List<BaseStat>();
    }

    public void AddStatBonus(BaseStat stat)
    {
        if (stat == null)
            return;
        foreach (var it in Bonuses)
        {
            if (it.affix == stat.affix)
            {
                it.value += stat.value;
            }
            else
            {
                this.Bonuses.Add(stat);
            }
            return;
        }
    }

    public void AddStatBonus(StatBonuses stats)
    {
        if (stats == null || stats.Bonuses.Count == 0)
            return;
        foreach (var it in stats.Bonuses)
        {
            AddStatBonus(it);
        }
    }

    public void SubstractStatBonus(BaseStat stat)
    {
        if (stat == null)
            return;
        foreach (var it in Bonuses)
        {
            if (it.affix == stat.affix)
            {
                it.value -= stat.value;
            }
            else
            {
                this.Bonuses.Add(new BaseStat(stat.affix, -stat.value));
            }
            return;
        }
    }

    public void SubstractStatBonus(StatBonuses stats)
    {
        if (stats == null || stats.Bonuses.Count == 0)
            return;
        foreach (var it in stats.Bonuses)
        {
            SubstractStatBonus(it);
        }
    }

    public float GetBonus(EquipmentAffixClass aff)
    {
        foreach(var it in Bonuses)
        {
            if (it.affix == aff)
                return it.value;
        }

        return 0;
    }
}
