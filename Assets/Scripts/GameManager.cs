using DefaultNamespace.Ads;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameObject startScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject failScreen;
    [SerializeField] private AnalyticManager analyticManager;//
    private InterstitialAdExample _interstitialAdExample;
    private RewardedAdsButton _rewardedAdsButton;

    private Level _level;
    enum GameState
    {
        None,
        Start,
        Game,
        Win,
        Fail
    }

    private GameState _state;

    private GameState State
    {
        get => _state;
        set
        {
            if(value == _state)
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
                case GameState.Fail:
                    screen = failScreen;
                    break;
                case GameState.Win:
                    screen = winScreen;
                    break;
            }
            OpenScreen(screen);
        }
       
    }

    private GameObject _currentScreen;

    #region UI Logic
    private void OpenScreen(GameObject screen)
    {
        if (_currentScreen)
        {
            _currentScreen.SetActive(false);
        }

        _currentScreen = screen;
        _currentScreen.SetActive(true);
    }
    
    #endregion

    private void Start()
    {
        _level = GetComponentInChildren<Level>();
        TurnOffAllScreens();
        State = GameState.Start;
        _level.GenerateLevel();
        _interstitialAdExample = FindObjectOfType<AdsManager>().GetComponent<InterstitialAdExample>();
        _rewardedAdsButton = FindObjectOfType<AdsManager>().GetComponent<RewardedAdsButton>();
    }

    private void TurnOffAllScreens()
    {
        startScreen.SetActive(false);
        gameScreen.SetActive(false);
        winScreen.SetActive(false);
        failScreen.SetActive(false);
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
        _rewardedAdsButton.ShowAd();
        _level.RestartLevel();
        StartGame();
        analyticManager.OnLevelStart();
    }

    public void NextLevel()
    {
        _interstitialAdExample.ShowAd();
        _level.GenerateLevel();
        StartGame();
        analyticManager.OnLevelWin();
    }
}
