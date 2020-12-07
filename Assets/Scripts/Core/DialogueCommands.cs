using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TrainMystery
{
    public class DialogueCommands : MonoBehaviour
    {
        public void DialogueCommand_DialogueStart()
        {
            Debug.Log("DialogueCommand_DialogueStart");
            TrainMysteryGameManager.Instance.SetGameState(GameState.dialogue);
            TrainMysteryGameManager.Instance.PausePlayerController();
        }

        public void DialogueCommand_DialogueEnd()
        {
            Debug.Log("DialogueCommand_DialogueEnd");
            TrainMysteryGameManager.Instance.UnpausePlayerController();
            TrainMysteryGameManager.Instance.SetGameState(GameState.running);
        }
    } 
}
