using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : NPC
{
    protected QuestManager questManager;

    [SerializeField] protected Quest NPCsquest;

    [SerializeField] QuestDialogue dialogue;

    [SerializeField] protected string[] dialogueText;

    public void Start()
    {
        questManager = FindObjectOfType<QuestManager>();

        if (questManager == null)
        {
            Debug.LogError("There is no QuestManager in the scene");
        }

        if (dialogue == null)
        {
            Debug.LogError("Quest dialogue not included");
        }

    }

    public override void Interact()
    {
        switch (NPCsquest.goal.questState)
        {
            case QuestState.Available:
                dialogue.npcName = name;
                dialogue.dialogueText = dialogueText;
                dialogue.quest = NPCsquest;
                dialogue.showDialogue = true;
                //questManager.AcceptQuest(NPCsquest);
                Debug.Log("Accepted");
                break;
            case QuestState.Active:
                if (NPCsquest.goal.IsCompleted())
                {
                    Debug.Log("Claimed");
                    questManager.ClaimQuest();
                }
                else
                {
                    Debug.Log("Not Claimed");
                }
                break;
            case QuestState.Claimed:
                //some dialogue
                //"you have already helped me enough"
                break;
        }
    }
}
