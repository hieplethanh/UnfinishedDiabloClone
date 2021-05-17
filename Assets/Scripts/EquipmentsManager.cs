using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentsManager
{
    public static EquipmentsManager instance;
    public StatBonuses ItemBonuses;
    public static EquipmentsManager GetInstance()
    {
        if (instance == null)
        {
            instance = new EquipmentsManager();
        }
        return instance;
    }

    public GameObject playerHand;
    public GameObject playerHead;
    public GameObject playerBody;
    public GameObject playerPants;

    public ItemBase [] AllEquipments;
    public CharacterStats characterStats;
    public StatBonuses itemBonuses;

    EquipmentsManager()
    {
        AllEquipments = new ItemBase[8]; // based on EquipmentType Enum, 0 = weapon, 1 = offhand, etc;
        characterStats = CharacterStats.GetInstance();
        itemBonuses = characterStats.ItemBonuses;
        LoadItems();
    }

    public void EquipItem(ItemBase newItem)
    {
        UnequipItem(newItem.itemType);
        if (newItem.itemType == EquipmentType.OneHandedWeapon)
        {
            AllEquipments[0] = newItem;
        }
        else
        {
            AllEquipments[(int)newItem.itemType - 2] = newItem;
        }
        itemBonuses.AddStatBonus(newItem.Stats);
        characterStats.CalculateAllBonuses();
    }

    public void LoadItems()
    {
        for (int i=0; i<8; i++)
        {
            AllEquipments[i] = ItemBase.GetRandomItem(EquipmentQuality.Normal, 1);
        }
        // load items from somewhere, something and add to AllEquipments
        foreach (var it in AllEquipments)
            foreach (var it2 in it.Stats.Bonuses)
            {
                ItemBonuses.AddStatBonus(it2);
            }
        characterStats.CalculateAllBonuses();
    }

    public ItemBase UnequipItem(EquipmentType type)
    {
        ItemBase item = null;
        if (type == EquipmentType.OneHandedWeapon)
        {
            ItemBonuses.SubstractStatBonus(AllEquipments[0].Stats);
            PlayerManager.GetInstance().characterStats.CalculateAllBonuses();
            item = AllEquipments[0];
        }

        if (type == EquipmentType.TwoHandedWeapon)
        {
            ItemBonuses.SubstractStatBonus(AllEquipments[0].Stats);
            PlayerManager.GetInstance().characterStats.CalculateAllBonuses();
            item = AllEquipments[0];
        }
        else
        {
            ItemBonuses.SubstractStatBonus(AllEquipments[(int)type-2].Stats);
            PlayerManager.GetInstance().characterStats.CalculateAllBonuses();
            item = AllEquipments[(int)type - 2];
        }
        return item;
    }
}
