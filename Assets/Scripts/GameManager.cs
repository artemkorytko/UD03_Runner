using UnityEngine;

enum GameState
{
    None,
    Start,
    Game,
    Win,
    Fail
}

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject StartScreen;
    [SerializeField] private GameObject GameScreen;
    [SerializeField] private GameObject WinScreen;
    [SerializeField] private GameObject FailScreen;

    private Level _level;
    private GameState _state;
    private GameObject _currentScreen;

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
                    screen = StartScreen;
                    break;
                case GameState.Game:
                    screen = GameScreen;
                    break;
                case GameState.Win:
                    screen = WinScreen;
                    break;
                case GameState.Fail:
                    screen = FailScreen;
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

    private void Start()
    {
        _level = GetComponentInChildren<Level>();
        TurnOffAllScreens();
        State = GameState.Start;
        _level.GenerateLevel();
    }

    private void TurnOffAllScreens()
    {
        StartScreen.SetActive(false);
        GameScreen.SetActive(false);
        WinScreen.SetActive(false);
        FailScreen.SetActive(false);
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
        _level.GenerateLevel();
        StartGame();
    }

    public void NextLevel()
    {
        _level.GenerateLevel();
        StartGame();
    }
} 

