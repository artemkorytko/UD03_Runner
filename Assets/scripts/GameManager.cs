using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    enum GameState
    {
        None,
        Start,
        Game,
        Win,
        Fail
    }
    
    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject failScreen;

    private Level _level;
    
    private GameObject _currentScreen;

    private GameState _state;

    private GameState State
    {
        get => _state;
        set
        {
            if (value == _state)
                return;
            _state = value;
            GameObject screen = null;
            switch (_state)
            {
                case GameState.Start:
                    screen = startScreen;
                    break;
                case GameState.Game:
                    screen = gameScreen;
                    break;
                case GameState.Win:
                    screen = winScreen;
                    break;
                case GameState.Fail:
                    screen = failScreen;
                    break;
            }
            OpenScreen(screen);
        }
    }
    

    private void OpenScreen(GameObject screen)
    {
        if (_currentScreen)
        {
            _currentScreen.SetActive(false);
        }

        _currentScreen = screen;
        _currentScreen.SetActive(true);
    }

    private void TurnOffAllScreens()
    {
        startScreen.SetActive(false);
        winScreen.SetActive(false);
        failScreen.SetActive(false);
        gameScreen.SetActive(false);
    }

    private void Start()
    {
        TurnOffAllScreens();
        State = GameState.Start;
        _level = GetComponentInChildren<Level>();
        _level.GenerateLevel();
    }

    public void StartGame()
    {
        State = GameState.Game;
        _level.Player.IsActive = true;
        _level.Player.OnDied += OnDead;
        _level.Player.OnFinish += OnWin;
    }

    private void OnDead()
    {
        State = GameState.Fail;
    }

    private void OnWin()
    {
        State = GameState.Win;
    }
    
    public void RestartGame()
    {
        StartGame();
    }
    
    public void NextLevel()
    {
        StartGame();
    }
}
