using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager: MonoBehaviour
{
    public static PlayerManager instance;
    public static PlayerManager GetInstance()
    {
        if (instance == null)
        {
            instance = new PlayerManager();
        }
        return instance;
    }

    public EquipmentsManager equipmentsManager { get; set; }
    public CharacterStats characterStats { get; set; }
    public HealthCalculator healthCalculator { get; set; }

    public PlayerManager()
    {
    }

    public void Start()
    {
        equipmentsManager = EquipmentsManager.GetInstance();
        characterStats = CharacterStats.GetInstance();
        healthCalculator = new HealthCalculator();

        characterStats.CalculateStats();
        healthCalculator.MaxHitPoint = characterStats.MaxHitPoint;
        healthCalculator.HitPoint = characterStats.MaxHitPoint;
    }
}
