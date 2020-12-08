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
    } 
}
