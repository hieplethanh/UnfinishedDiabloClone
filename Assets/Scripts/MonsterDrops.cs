using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDrops
{
    public int[,] ItemDropRate = new int [,]
    {// Item rarity = Normal, Magic, Rare, Legendary, percentage
        { 80,  20,  10, 0}, // Normal monsters
        { 10, 250,  70, 0},
        {  0, 100, 150, 0}
    };

    public List<ItemBase> drops = new List<ItemBase>();
    MonsterDrops(MonsterRank rank, int level)
    {
        drops = new List<ItemBase>();
        for (int rarity = 0; rarity<3; rarity++)
        {
            int dropRate = ItemDropRate[(int)rank, rarity];
            int result = Random.Range(1, dropRate);
            while (result>=0)
            {
                if (result>100 || result<= dropRate%100)
                {
                    drops.Add(ItemBase.GetRandomItem((EquipmentQuality)rarity, level));
                }
            }
        }
    }
}
