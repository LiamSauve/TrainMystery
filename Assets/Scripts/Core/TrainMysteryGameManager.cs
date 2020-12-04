using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    running = 0,
    dialogue = 1,
    menu = 2
}

public class TrainMysteryGameManager : MonoBehaviour
{
    private static TrainMysteryGameManager _instance;
    public static TrainMysteryGameManager Instance { get { return _instance; } }

    private GameState state = GameState.running;

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
        switch(_instance.state)
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

    public GameState GetGameState()
    {
        return state;
    }
}
