using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class NPC : Interactable
{
    public YarnProgram yarnProgram;

    private void Start()
    {
        if (yarnProgram != null)
        {
            DialogueRunner dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
            dialogueRunner.Add(yarnProgram);
        }
    }

    public override void Interact()
    {
        //FindObjectOfType<DialogueRunner>().StartDialogue("Start");
        Debug.Log("Interacting with me!");
    }
}
