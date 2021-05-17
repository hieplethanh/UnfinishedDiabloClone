using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBase
{
    public EquipmentQuality itemQuality { get; set; }
    public EquipmentType itemType { get; set; }
    public int itemLevel { get; set; }
    public StatBonuses Stats { get; set; }
    public int[] BonusStatsAmount = {0, 2, 4, 6}; // Normal = 0 stats, rare = 6;

    public static ItemBase GetRandomItem(EquipmentQuality qual, int lvl)
    {
        EquipmentType type = (EquipmentType)Random.Range((int)EquipmentType.OneHandedWeapon, (int)EquipmentType.Boot);
        ItemBase temp = new ItemBase(type, qual, lvl);
        temp.GenerateRandomAffixes();
        return temp;
    }

    public ItemBase(EquipmentType type, EquipmentQuality qual, int lvl)
    {
        itemType = type;
        itemQuality = qual;
        itemLevel = lvl;
        Stats = new StatBonuses();
    }

    public void GenerateRandomAffixes()
    {
        Stats.Bonuses = new List<BaseStat>();
        GenerateBaseAffixes();
        GenerateBonusAffixes();
    }
    public void GenerateBaseAffixes()
    {
        switch (itemType) {
            case EquipmentType.OneHandedWeapon:
                Stats.AddStatBonus(BaseStat.GetRandomizeStat(EquipmentAffixClass.WeaponDamage, 5 + itemLevel*2, 10 + itemLevel * 2));
                //Stats.AddStatBonus(BaseStat.GetRandomizeStat(EquipmentAffixClass.AttackSpeed, 50, 100));
                break;
            case EquipmentType.TwoHandedWeapon:
                Stats.AddStatBonus(BaseStat.GetRandomizeStat(EquipmentAffixClass.WeaponDamage, 10 + itemLevel * 3, 15 + itemLevel * 5));
                //Stats.AddStatBonus(BaseStat.GetRandomizeStat(EquipmentAffixClass.AttackSpeed, 1, 20));
                break;
            case EquipmentType.Shield:
                Stats.AddStatBonus(BaseStat.GetRandomizeStat(EquipmentAffixClass.Armor, itemLevel + 5, itemLevel * 1.5f + 10));
                Stats.AddStatBonus(BaseStat.GetRandomizeStat(EquipmentAffixClass.BonusHealth, itemLevel*2 + 10, itemLevel*2.5f + 20));
                break;
            case EquipmentType.Quiver:
                Stats.AddStatBonus(BaseStat.GetRandomizeStat(EquipmentAffixClass.WeaponDamage, itemLevel*0.5f, itemLevel*0.5f+3));
                break;
            case EquipmentType.BodyArmor:
                Stats.AddStatBonus(BaseStat.GetRandomizeStat(EquipmentAffixClass.Armor, itemLevel + 5, itemLevel * 1.5f + 10));
                Stats.AddStatBonus(BaseStat.GetRandomizeStat(EquipmentAffixClass.BonusHealth, itemLevel * 2 + 10, itemLevel * 2.5f + 20));
                break;
            case EquipmentType.Helm:
            case EquipmentType.Pants:
            case EquipmentType.Gloves:
            case EquipmentType.Boot:
                Stats.AddStatBonus(BaseStat.GetRandomizeStat(EquipmentAffixClass.Armor, itemLevel * 0.5f + 2, itemLevel + 5));
                break;
        }
    }
    public void GenerateBonusAffixes()
    {
        int additionalBonuses = BonusStatsAmount[(int)itemQuality];
        for (int i=0; i< additionalBonuses; i++)
        {
            bool hasAffix = true;
            while (hasAffix) // randomize until there're not 2 similar affixes
            {
                BaseStat newStat = BaseStat.GetRandomizeStat((EquipmentAffixClass)0, (EquipmentAffixClass)14, itemLevel);

                hasAffix = false;

                foreach (var it in Stats.Bonuses)
                {
                    if (it.affix == newStat.affix)
                        hasAffix = true;
                }
            }
        }
    }

    public void LoadBonuses(StatBonuses stb)
    {
        Stats.Bonuses = new List<BaseStat>();
        foreach (var it in stb.Bonuses)
        {
            Stats.AddStatBonus(it);
        }
    }
}


