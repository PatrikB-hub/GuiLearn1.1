using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GatherQuestGoal : QuestGoal
{
    #region Gather variables
    public Inventory playerInventory;

    public string itemName;// item Id name
    public int requiredAmount;
    #endregion

    private void Start()
    {
        playerInventory = (Inventory)GameObject.FindObjectOfType<Inventory>();
        if (playerInventory == null)
        {
            Debug.LogError("There is no player with an inventory in the scene");

        }
    }


    public override bool IsCompleted()
    {
        Item item = playerInventory.FindItem(itemName);

        if (item == null)
        {
            return false;
        }

        if (item.Amount >= requiredAmount)
        {
            return true;
        }

        return false;
    }
}
