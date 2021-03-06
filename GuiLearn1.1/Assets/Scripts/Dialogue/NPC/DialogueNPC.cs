﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPC : NPC
{
    [SerializeField] private Dialogue dialogue;

    [SerializeField] protected string[] dialogueText;

    public override void Interact()
    {
        dialogue.npcName = name;
        dialogue.dialogueText = dialogueText;

        // set up dialogue
        dialogue.showDialogue = true;

        //Debug.Log("Dialogue NPC");
    }
}
