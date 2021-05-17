public enum EquipmentType
{
    OneHandedWeapon,
    TwoHandedWeapon,
    Shield,
    Quiver,
    BodyArmor,
    Helm,
    Pants,
    Gloves,
    Boot
}

public enum EquipmentQuality
{
    Normal,
    Magic,
    Rare,
    Legendary
}

public enum WeaponClass
{
    BluntWeapon,
    Sword,
    Axe,
    Bow
}

public enum EquipmentAffixClass
{
    WeaponDamage = 0, // Damage 0->2
    AttackSpeed,
    BonusDamagePercent,

    Armor, // armors 3->5
    BonusHealth,
    DamageReduction,

    FireResistance, // resist 6->10
    ColdResistance,
    LightningResistance,
    MagicResistance,
    PoisonResistance,

    BonusStrength,// common 11->14
    BonusVitality,
    BonusAgility,
    BonusIntelligent
}