using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

namespace TrainMystery
{

    public enum GameState
    {
        running = 0,
        dialogue = 1,
        menu = 2
    }

    public class TrainMysteryGameManager : MonoBehaviour
    {
        public static TrainMysteryGameManager Instance { get { return _instance; } }

        private static TrainMysteryGameManager _instance;
        private GameState _state = GameState.running;
        [SerializeField]
        private GameObject _player;

        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }
        }

        public bool IsPaused()
        {
            bool isPaused = false;
            switch (_instance._state)
            {
                case GameState.dialogue:
                case GameState.menu:
                    isPaused = true;
                    break;
                default:
                    isPaused = false;
                    break;
            }

            return isPaused;
        }

        public void SetGameState(GameState gameState)
        {
            _state = gameState;
        }

        public GameState GetGameState()
        {
            return _state;
        }

        public void PausePlayerController()
        {
            var fpsController = _player.GetComponent<FirstPersonAIO>();
            if(fpsController.controllerPauseState == true)
            {
                return;
            }

            fpsController.ControllerPause();
        }

        public void UnpausePlayerController()
        {
            _player.GetComponent<FirstPersonAIO>().ControllerPause();
        }

        // game progress values
        public bool hasGun { get; private set; }
        public bool talkedToCharles { get; private set; }
        public void HasGun()
        {
            hasGun = true;
            _player.GetComponent<Player>().EquipGun();
        }
        public void TalkedToCharles()
        {
            talkedToCharles = true;
        }
    } 
}
