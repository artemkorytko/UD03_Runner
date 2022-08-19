using System;
using System.Collections;
using System.Collections.Generic;
using ADS_scripts;
using Unity.Services.Analytics;
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

    private DestroyableWall _destrWall;

    private InterstitialAd _interAD;
    private RewardedAdsButton _rewardedAdsButton;
    private int _loseCount;
    private int _winCount;
   
    

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
        _level = GetComponentInChildren<Level>();
        TurnOffAllScreens();
        State = GameState.Start;
        _level.GenerateLevel();
         _interAD = FindObjectOfType<AdsManager>().GetComponent<InterstitialAd>();
         _rewardedAdsButton = FindObjectOfType<AdsManager>().GetComponent<RewardedAdsButton>();
         _winCount = 0;
         _loseCount = 0;
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
        var data = new Dictionary<string, object>
        {
            { "lost_count", _loseCount++ }
        };
        AnalyticsService.Instance.CustomData("LostEvent", data);
        AnalyticsService.Instance.Flush();
        
        _rewardedAdsButton.LoadAd();
        State = GameState.Fail;
        
        
    }

    private void OnWin()
    {
        var data = new Dictionary<string, object>
        {
            { "win_count", _winCount++ }
        };
        AnalyticsService.Instance.CustomData("WinEvent", data);
        AnalyticsService.Instance.Flush();
        
        _interAD.LoadAd();
        State = GameState.Win;
    }
    
    public void RestartGame()
    {
        _level.GeneratePlayer();
        _level.ReInstatiateDestrWalls();
        State = GameState.Start;

    }
    
    public void NextLevel()
    {
        _level.GenerateLevel();
        StartGame();
    }
}
