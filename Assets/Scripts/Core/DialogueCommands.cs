using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

namespace TrainMystery
{
    public class DialogueCommands : MonoBehaviour
    {
        [SerializeField]
        DialogueRunner dialogueRunner;

        private void Awake()
        {
            dialogueRunner.AddCommandHandler("DialogueCommand_Test", DialogueCommand_Test);
            dialogueRunner.AddCommandHandler("DialogueCommand_AddNoteTest", DialogueCommand_AddNoteTest);
        }

        // Start/End
        public void DialogueCommand_DialogueStart()
        {
            TrainMysteryGameManager.Instance.SetGameState(GameState.dialogue);
            TrainMysteryGameManager.Instance.PausePlayerController();
        }

        public void DialogueCommand_DialogueEnd()
        {
            TrainMysteryGameManager.Instance.UnpausePlayerController();
            TrainMysteryGameManager.Instance.SetGameState(GameState.running);
        }


        // commands from dialogue
        private void DialogueCommand_Test(string[] parameters)
        {
            if (parameters.Length > 0)
            {
                Debug.Log(parameters[0]);
            }
        }

        private void DialogueCommand_AddNoteTest(string[] parameters)
        {
            //if (parameters.Length > 0)
            //{
            //    Debug.Log(parameters[0]);
            //}
            var note = string.Empty;

            foreach(var s in parameters)
            {
                //if(string.Equals(s, string.Empty))
                //{
                //    return;
                //}

                note += s + " ";
            }

            if (string.Equals(note, string.Empty))
            {
                return;
            }

            TrainMysteryGameManager.Instance.uiCommands.AddNote(note);
        }

        private void DialogueCommand_EndGame(string[] parameters) // accused
        {
            TrainMysteryGameManager.Instance.EndGame();
        }
    } 
}
