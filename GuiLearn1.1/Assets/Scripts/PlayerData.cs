using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public Stats stats;
    public PlayerProfession profession;
    public int[] customisationTextureIndex;

    public PlayerData(Player player)
    {
        stats = player.playerStats.stats;
        profession = player.Profession;
        customisationTextureIndex = player.customisationTextureIndex;
    }

}
