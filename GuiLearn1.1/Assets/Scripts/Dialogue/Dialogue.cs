﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    #region Variables

    [Header("References")]
    public bool showDialogue;
    //index for the currnt line of dialogue
    public int currentLineIndex;

    public ThirdPersonMovement playerMovement;


    public Vector2 scr;

    [Header("NPC Name and Dialogue")]
    //name of the specific NPC talking
    public string npcName;
    //array for text of the dialogue
    public string[] dialogueText;

    #endregion

    protected virtual void OnGUI()
    {
        if (showDialogue)
        {
            playerMovement.enabled = false;
            scr.x = Screen.width / 16;
            scr.y = Screen.height / 9;

            //the dialogue box takes up the whole bottom 3rd
            //of the screen and displays the NPC's name
            //and current dialogue line
            GUI.Box(new Rect(0, 6 * scr.y,
                             Screen.width, scr.y * 3),
                             npcName + " : " + dialogueText[currentLineIndex]);

            //if not at the end of the dialogue
            if (currentLineIndex < dialogueText.Length - 1)
            {
                //next button allows you to skip forward to the next line or dialogue
                if (GUI.Button(new Rect(15 * scr.x, 8.5f * scr.y,
                                        scr.x, scr.y * 0.5f), "Next"))
                {
                    currentLineIndex++;
                }
            }
            else
            {
                EndDialogue();
            }


        }
    }

    protected virtual void EndDialogue()
    {
        if (GUI.Button(new Rect(15 * scr.x, 8.5f * scr.y,
                                            scr.x, scr.y * 0.5f), "Bye"))
        {
            showDialogue = false;
            currentLineIndex = 0;

            playerMovement.enabled = true;

            //Cursor.lockState = CursorLockMode.Locked;
            //Cursor.visible = false;

        }
    }


}
