﻿using Yarn.Unity;

namespace TrainMystery
{
    public class Interactable_NPC : Interactable
    {
        public string startNode;
        public YarnProgram yarnProgram;
        DialogueRunner dialogueRunner;
        InMemoryVariableStorage variableStorage;
        CharacterData characterData;
        public int charID;

        private void Start()
        {
            dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
            variableStorage = dialogueRunner.GetComponent<InMemoryVariableStorage>();
            characterData = dialogueRunner.GetComponent<CharacterData>();
            if (yarnProgram != null)
            {
                dialogueRunner.Add(yarnProgram);
            }
        }

        public override void Interact()
        {
            TrainMysteryGameManager.Instance.uiCommands.SetFacedObjectLabel(string.Empty);
            //variableStorage.SetValue("$convname", characterData.dialogueStrings[charID*3]); //set name (yarn vars in code must have $ at the start, but not in the inspector)
            //variableStorage.SetValue("$convdesc", characterData.dialogueStrings[charID*3+1]); //set description
            //variableStorage.SetValue("$convhand", characterData.dialogueStrings[charID*3+2]); //set handedness

            variableStorage.SetValue("$convname", characterData.dialogueStrings[charID].name);
            variableStorage.SetValue("$convdesc", characterData.dialogueStrings[charID].description);
            variableStorage.SetValue("$convhand", characterData.dialogueStrings[charID].handedness);
            variableStorage.SetValue("$convhandq", characterData.dialogueStrings[charID].handQ);
            variableStorage.SetValue("$convwrite", characterData.dialogueStrings[charID].writing);
            variableStorage.SetValue("$convwriteq", characterData.dialogueStrings[charID].writeQ);
            variableStorage.SetValue("$convcloth", characterData.dialogueStrings[charID].cloth);
            variableStorage.SetValue("$convclothq", characterData.dialogueStrings[charID].clothQ);
            variableStorage.SetValue("$convid", charID);

            dialogueRunner.StartDialogue(startNode);
        }
    } 
}
