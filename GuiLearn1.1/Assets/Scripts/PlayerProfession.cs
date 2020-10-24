using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerProfession
{
    public string professionName = "Profession";

    public string abilityName = "ability";
    public string abilityDescription = "Does an action";

    public BaseStats[] defaultStats;
}
