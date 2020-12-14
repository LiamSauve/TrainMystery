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
            dialogueRunner.AddCommandHandler("DialogueCommand_AddNote", DialogueCommand_AddNote);
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

        private void Update()
        {
            if(dialogueRunner.IsDialogueRunning)
            {
                if(Input.GetMouseButtonDown(0))
                {
                    gameObject.GetComponent<DialogueUI>().MarkLineComplete();
                }
            }
        }


        // commands from dialogue
        private void DialogueCommand_Test(string[] parameters)
        {
            if (parameters.Length > 0)
            {
                Debug.Log(parameters[0]);
            }
        }

        private void DialogueCommand_AddNote(string[] parameters)
        {
            //if (parameters.Length > 0)
            //{
            //    Debug.Log(parameters[0]);
            //}
            var index = int.Parse(parameters[0]);
            var note = string.Empty;

            bool skipFirstParam = false;
            foreach(var s in parameters)
            {
                if(!skipFirstParam)
                {
                    skipFirstParam = true;
                    continue;
                }
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

            TrainMysteryGameManager.Instance.uiCommands.AddNote(index, note);
        }

        private void DialogueCommand_EndGame(string[] parameters) // accused
        {
            TrainMysteryGameManager.Instance.EndGame();
        }
    } 
}
