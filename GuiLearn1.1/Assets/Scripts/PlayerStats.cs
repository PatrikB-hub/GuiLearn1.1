using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Stores the Players Stats 
/// </summary>
///
/// 

[System.Serializable]
public struct BaseStats
{
    public string baseStatName;
    public int defaultStat; // stat from the class
    public int levelUpStat;
    public int additionalStat; // additional stat from
    // final stats will be 
    // default + additional

    public int finalStat
    {
        get
        {
            return defaultStat + additionalStat + levelUpStat;
        }
    }
}

[System.Serializable]
public class Stats
{
    [Header("Player Movement")]
    [SerializeField] public float speed = 6f;
    [SerializeField] public float sprintSpeed = 12f;
    [SerializeField] public float crouchSpeed = 3f;
    [SerializeField] public float jumpHeight = 1.0f;

    [Header("Current Stats")]
    [SerializeField] public int level;

    [SerializeField] public float maxHealth = 100;
    [SerializeField] public float regenHealth = 5f;//---
    [SerializeField] public float currentMana = 100;
    [SerializeField] public float maxMana = 100;
    [SerializeField] public float currentStamina = 100;
    public float regenStamina = 5f;
    [SerializeField] public float maxStamina = 100;

    [Header("Base Stats")]
    public int baseStatPoints = 10;
    public BaseStats[] baseStats;
}

[System.Serializable]
public class PlayerStats
{
    public Stats stats;

    //getter setter
    private float _currentHealth = 100;// field
    public float CurrentHealth // property
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = Mathf.Clamp(value, 0, stats.maxHealth);
            
            if (healthHearts != null)
            {
                healthHearts.UpdateHearts(value, stats.maxHealth);
            }

        }
    }
    public QuaterHearts healthHearts;


    public bool SetStats(int statIndex, int amount)
    {
        
        if (amount > 0 //increasing
            //we cant add points if there are none left
            && stats.baseStatPoints - amount < 0)
        {
            return false;
        }
        //decreasing
        else if (amount < 0 
            // additionalStat must be 0 or positive int
            && stats.baseStats[statIndex].additionalStat + amount < 0)
        {
            return false;
        }

        //change the stat
        stats.baseStats[statIndex].additionalStat += amount;
        stats.baseStatPoints -= amount;

        return true;
    }
}
