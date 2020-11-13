using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum QuestState
{
    Available,
    Active,
    Claimed
}

public enum GoalType
{
    Gather,
    //Kill,
    //Escort,
    //Locate
}

[System.Serializable]
public abstract class QuestGoal : MonoBehaviour
{
    public QuestState questState;

    public GoalType goalType;

    

    public abstract bool IsCompleted();

    /*public void ItemCollected(string name)
    {
        if (goalType == GoalType.Gather && itemName == name)
        {
            currentAmount++;
            if (currentAmount >= requiredAmount)
            {
                questState = QuestState.Completed;
                Debug.Log("QUEST COMPLETE");
            }
        }
    }*/
}
