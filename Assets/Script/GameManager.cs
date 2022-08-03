using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DefaultNameSpace
{



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
        private GameObject _curretScreen;

        #region UI Logic
        private void OpenScreen(GameObject screen)
        {
            if (_curretScreen)
            {
                _curretScreen.SetActive(false);


            }
            _curretScreen = screen;
            _curretScreen.SetActive(true);
              
        }
        #endregion

        private void Start()

        {
            TurnOffAllScreens();

            State = GameState.Start;

        }
        private void TurnOffAllScreens()
        {
            startScreen.SetActive(false);
            gameScreen.SetActive(false);
            winScreen.SetActive(false);
            failScreen.SetActive(false);

        }
        public  void StartGame()
        {
            State = GameState.Game;
        }
        private void OnDead()
        {
            State = GameState.Fail;
        }
        private void OnWin()
        {
            State = GameState.Win;
        }

        private void RestartGame()
        {
            StartGame();

        }
        public void NextLevel()
        {
            StartGame();
        }



    }





}
