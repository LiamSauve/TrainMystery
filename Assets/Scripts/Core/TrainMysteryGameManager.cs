using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using Yarn.Unity;
using DG.Tweening;

namespace TrainMystery
{

    public enum GameState
    {
        running = 0,
        intro = 1,
        dialogue = 2,
        menu = 3
    }

    public class TrainMysteryGameManager : TimeScaleIndependentUpdate
    {
        public static TrainMysteryGameManager Instance { get { return _instance; } }

        private static TrainMysteryGameManager _instance;
        private GameState _state = GameState.running;
        [SerializeField]
        private GameObject _player;
        [SerializeField]
        public InMemoryVariableStorage yarnVariables;
        [SerializeField]
        public UICommands uiCommands;
        [SerializeField]
        public Introduction introduction;
        [SerializeField]
        public GameOver gameOver;

        public GameObject[] treeEmitters;

        private bool startGameTimer = false;
        [SerializeField]
        public float timer = 60f * 5f + 1; // 5 minutes

        private bool doCommenceGameOverTrainStation = false;
        private bool doCommenceGameOver = false;
        private float _currentTrainVolume = 1;

        protected override void Awake()
        {
            base.Awake();
            if (_instance != null && _instance != this)
            {
                Destroy(this.gameObject);
            }
            else
            {
                _instance = this;
            }

            BeginIntroduction();
        }

        protected override void Update()
        {
            base.Update();

            UpdateTimer();
        }

        public Player GetPlayer()
        {
            return _player.GetComponent<Player>();
        }

        private void UpdateTimer()
        {
            if(startGameTimer)
            {
                timer -= deltaTime;

                if (timer > 0.01)
                {
                    uiCommands.UpdateTimer(timer);
                }
            }

            if(!doCommenceGameOverTrainStation && timer < 30f)
            {
                doCommenceGameOverTrainStation = true;

                SlowTrainDown();
                DisableTreeEmitters();
                GenerateTrainStation();
            }

            if(timer < 0)
            {
                if(doCommenceGameOver == false) // game over timer
                {
                    doCommenceGameOver = true;
                    GameOver_TimeOut();
                }
            }
        }

        public void GameOver_User()
        {
            startGameTimer = false;
            PausePlayerController();

            uiCommands.SetFacedObjectLabel(string.Empty);
            uiCommands.ShowNotebookInput(false);
            uiCommands.ShowGunInput(false);
            uiCommands.ShowShootInput(false);
            uiCommands.ShowTimer(false);

            // no fancy UI stuff, just quit :)
            DOVirtual.DelayedCall(3, () => Application.Quit());
        }

        private void GameOver_TimeOut()
        {
            startGameTimer = false;
            PausePlayerController();

            uiCommands.SetFacedObjectLabel(string.Empty);
            uiCommands.ShowNotebookInput(false);
            uiCommands.ShowGunInput(false);
            uiCommands.ShowShootInput(false);
            uiCommands.ShowTimer(false);

            gameOver.Begin_TimeOut();
        }

        public void GameOver_Murderer(string murdereredName)
        {
            startGameTimer = false;
            PausePlayerController();

            uiCommands.SetFacedObjectLabel(string.Empty);
            uiCommands.ShowNotebookInput(false);
            uiCommands.ShowGunInput(false);
            uiCommands.ShowShootInput(false);
            uiCommands.ShowTimer(false);

            gameOver.Begin_Murderer(murdereredName);
        }

        public void GameOver_ShotNothing()
        {
            startGameTimer = false;
            PausePlayerController();

            uiCommands.SetFacedObjectLabel(string.Empty);
            uiCommands.ShowNotebookInput(false);
            uiCommands.ShowGunInput(false);
            uiCommands.ShowShootInput(false);
            uiCommands.ShowTimer(false);

            gameOver.Begin_ShotNothing();
        }

        private void DisableTreeEmitters()
        {
            foreach(var v in treeEmitters)
            {
                if(v != null)
                {
                    var emitter = v.GetComponent<TreeEmitter>();
                    emitter.End();
                }
            }
        }

        private void SlowTrainDown() // 30 seconds
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Append( DOTween.To(() => _currentTrainVolume, x => _currentTrainVolume = x, 0, 30));
            sequence.OnUpdate(() => UpdateTrainVolume() );

            DOVirtual.DelayedCall(15, () => AkSoundEngine.PostEvent("Play_sfx_train_steamwhistle", Camera.main.gameObject));
        }

        private void UpdateTrainVolume()
        {
            AkSoundEngine.SetRTPCValue("rtpc_train_speed", _currentTrainVolume);
        }

        private void GenerateTrainStation()
        {

        }

        private void DisableSounds()
        {

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

        private void BeginIntroduction()
        {

            if(!introduction.gameObject.activeInHierarchy)
            {
                startGameTimer = true;
                return;
            }

            SetGameState(GameState.intro);
            introduction.Begin();
            PausePlayerController();
        }

        public void EndIntroduction()
        {
            SetGameState(GameState.running);
            UnpausePlayerController();
            startGameTimer = true;
        }

        public void EndGame()
        {

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

        // game progress
        public void GotGun()
        {
            yarnVariables.SetValue("$HasGun", true);
            uiCommands.ShowGunInput(true);
            uiCommands.SetFacedObjectLabel(string.Empty);

            _player.GetComponent<Player>().EquipGun();
        }
        public void GotNotebook()
        {
            yarnVariables.SetValue("$HasNotebook", true);
            uiCommands.ShowNotebookInput(true);
            uiCommands.SetFacedObjectLabel(string.Empty);

            _player.GetComponent<Player>().EquipNotebook();
        }
        public void TalkedToCharles()
        {
        }
    } 
}
