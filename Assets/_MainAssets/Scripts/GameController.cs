using System;
using _MainAssets.Scripts.Player.Entity;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _MainAssets.Scripts
{
    public class GameController : MonoBehaviour
    {
        #region Singleton

        private static GameController _instance;

        public static GameController Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<GameController>();
                if (_instance == null)
                    Debug.LogError("GameController not found");
                return _instance;
            }
        }

        private void CheckInstance()
        {
            if (_instance == null)
            {
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(this);
            }
        }

        #endregion

        #region Events

        public static Action gameStarted;
        public static Action levelFailed;
        public static Action enemiesDestroyed;
        public static Action levelPassed;
        public static Action gameEnded;


        public bool GameStarted { get; private set; }
        public bool LevelFailed { get; private set; }
        public bool EnemiesDestroyed { get; private set; }
        public bool LevelPassed { get; private set; }
        public bool GameEnded { get; private set; }

        public void StartGame()
        {
            if (!GameStarted)
            {
                Time.timeScale = 1f;
                GameStarted = true;
                gameStarted.Invoke();
            }
        }

        public void LevelFail()
        {
            if (!LevelFailed)
            {
                LevelFailed = true;
                levelFailed?.Invoke();
            }
        }

        public void EnemiesDestroyComplete()
        {
            if (!EnemiesDestroyed)
            {
                
                EnemiesDestroyed = true;
                enemiesDestroyed?.Invoke();
            }
        }


        

        public void LevelPass()
        {
            if (!LevelPassed)
            {
                LevelPassed = true;
                levelPassed?.Invoke();
            }
        }

     

        public void EndGame()
        {
            if (!GameEnded)
            {
                GameEnded = true;
                gameEnded?.Invoke();
            }
        }

        #endregion

        private GameModel _gameModel;

        private GameModel m_GameModel
        {
            get
            {
                if (_gameModel == null)
                    _gameModel = GetComponent<GameModel>();
                return _gameModel;
            }
        }

        private PlayerModel _playerModel;

        private void OnEnable()
        {
            CheckInstance();
            _playerModel = FindObjectOfType<PlayerModel>();
            _playerModel.Dead += LevelFail;
            levelFailed += EndGame;
            levelPassed += EndGame;
        }

        private void OnDisable()
        {
            _playerModel.Dead -= LevelFail;
            levelFailed -= EndGame;
            levelPassed -= EndGame;
        }

        public GameObject GetPlayer()
        {
            return m_GameModel.Player;
        }

        public void Repeat()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}