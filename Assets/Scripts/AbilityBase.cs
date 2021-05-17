using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityBase : MonoBehaviour
{
    float CooldownStartTime;
    public float Cooldown = 0;
    public float GetCooldownTime()
    {
        return Time.time - CooldownStartTime;
    }

    public float GetCooldownPercentage()
    {
        return GetCooldownTime() / Cooldown;
    }

    public void StartCooldown()
    {
        CooldownStartTime = Time.time;
    }

    virtual public void UseAbility()
    {
    }
}
