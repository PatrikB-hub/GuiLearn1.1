using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDialogue : Dialogue
{
    QuestManager questManager;

    [HideInInspector]
    public Quest quest;

    public void Start()
    {
        questManager = FindObjectOfType<QuestManager>();

        if (questManager == null)
        {
            Debug.LogError("There is no QuestManager in the scene");
        }
    }

    protected override void EndDialogue()
    {
        if (GUI.Button(new Rect(15 * scr.x, 8.5f * scr.y,
                                            scr.x, scr.y * 0.5f), "Accept"))
        {
            Debug.Log("Accept quest here");

            questManager.AcceptQuest(quest);

            showDialogue = false;
            currentLineIndex = 0;

            playerMovement.enabled = true;
        }

        if (GUI.Button(new Rect(13f * scr.x, 8.5f * scr.y,
                                            scr.x, scr.y * 0.5f), "Don't Accept"))
        {
            Debug.Log("Accept quest here");

            showDialogue = false;
            currentLineIndex = 0;

            playerMovement.enabled = true;
        }
    }
}
