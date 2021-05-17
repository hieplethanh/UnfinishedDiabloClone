using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats
{
    public static CharacterStats instance;
    public static CharacterStats GetInstance()
    {
        if (instance == null)
        {
            instance = new CharacterStats();
        }
        return instance;
    }

    public StatBonuses BaseBonuses = new StatBonuses();
    public StatBonuses ItemBonuses = new StatBonuses();
    public StatBonuses BuffBonuses = new StatBonuses();
    public StatBonuses TotalBonuses = new StatBonuses();

    public float MaxHitPoint;
    public float TotalDmgBonus;
    public float[] DamageTypeReduction = { 0, 0, 0, 0, 0, 0 }; // Physical = 0, Fire, Cold, Lightning, Magic, Poison


    private float ResistBreakPoint = 100;
    private float healthPerVit = 5.0f;
    private float damageBonusPerStr = 1f;
    private float BonusArmorPerAgi = 0.1f;
    CharacterStats()
    {
        LoadSaveGame();
        CalculateAllBonuses();
        CalculateStats();
    }
    public void CalculateAllBonuses()
    {
        TotalBonuses = new StatBonuses();
        foreach (var it in BaseBonuses.Bonuses)
        {
            TotalBonuses.AddStatBonus(it);
        }

        foreach (var it in ItemBonuses.Bonuses)
        {
            TotalBonuses.AddStatBonus(it);
        }

        foreach (var it in BuffBonuses.Bonuses)
        {
            TotalBonuses.AddStatBonus(it);
        }
    }

    public void LoadSaveGame()
    {
        BaseBonuses.AddStatBonus(new BaseStat(EquipmentAffixClass.BonusStrength, 20)); // later need to load from save file.
        BaseBonuses.AddStatBonus(new BaseStat(EquipmentAffixClass.BonusAgility, 10));
        BaseBonuses.AddStatBonus(new BaseStat(EquipmentAffixClass.BonusVitality, 15));
        BaseBonuses.AddStatBonus(new BaseStat(EquipmentAffixClass.BonusIntelligent, 10));

        BaseBonuses.AddStatBonus(new BaseStat(EquipmentAffixClass.WeaponDamage, 3));
        BaseBonuses.AddStatBonus(new BaseStat(EquipmentAffixClass.WeaponDamage, 3));
        BaseBonuses.AddStatBonus(new BaseStat(EquipmentAffixClass.WeaponDamage, 3));
        BaseBonuses.AddStatBonus(new BaseStat(EquipmentAffixClass.WeaponDamage, 3));
        BaseBonuses.AddStatBonus(new BaseStat(EquipmentAffixClass.WeaponDamage, 3));
        BaseBonuses.AddStatBonus(new BaseStat(EquipmentAffixClass.WeaponDamage, 3));
        BaseBonuses.AddStatBonus(new BaseStat(EquipmentAffixClass.WeaponDamage, 3));
    }

    public void CalculateStats()
    {
        CalculateBaseBonuses();
        CalculateResistance();
    }

    public void CalculateBaseBonuses()
    {
        MaxHitPoint = healthPerVit * TotalBonuses.GetBonus(EquipmentAffixClass.BonusVitality);
        TotalDmgBonus = TotalBonuses.GetBonus(EquipmentAffixClass.BonusDamagePercent) + damageBonusPerStr * TotalBonuses.GetBonus(EquipmentAffixClass.BonusStrength);
    }
    public void CalculateResistance()
    {
        DamageTypeReduction[0] = CalculateDamageReductionFromResist(TotalBonuses.GetBonus(EquipmentAffixClass.Armor));
        for (int i=1; i<DamageTypeReduction.Length; i++)
        {
            DamageTypeReduction[i] = CalculateDamageReductionFromResist(TotalBonuses.GetBonus(EquipmentAffixClass.DamageReduction + i));
            PlayerManager.GetInstance().healthCalculator.DamageTypeReduction[i] = DamageTypeReduction[i];
        }
    }

    private float CalculateDamageReductionFromResist(float resist)
    {
        return (resist) / (resist + ResistBreakPoint);
    }
}
