using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCalculator : MonoBehaviour
{
    public float HitPoint;
    public float MaxHitPoint;

    public float[] DamageTypeReduction = { 0, 0, 0, 0, 0, 0 }; // Physical = 0, Fire, Cold, Lightning, Magic, Poison
    void OnAwake()
    {
        HitPoint = MaxHitPoint;
    }

    void Update()
    {
        if (HitPoint<=0)
        {
            if (gameObject.tag == "Player")
            {
                // Game over
            }

            else
            {
                Destroy(gameObject);
            }
        }
    }
    public void TakeDamage(float damage, DamageType dmgType)
    {
        HitPoint -= (damage * (1 - DamageTypeReduction[(int)dmgType]));
    }


}
