using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataManager : MonoBehaviour
{
    [SerializeField] private PlayerDeath _playerDeath;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private CoinManager _coinManager;
    private GameData _gameData = new GameData();

    private void Awake()
    {
        SaveSystem.CreateDataFile(_gameData);
        LoadData();
    }

    private void OnEnable()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
            _playerDeath.OnPlayerDied += OnDied;
    }

    private void OnDisable()
    {
        if (SceneManager.GetActiveScene().name == "GameScene")
            _playerDeath.OnPlayerDied -= OnDied;
    }

    private void OnDied()
    {
        SaveData();
    }

    public void SaveData()
    {
        _gameData.highscore = _scoreManager.HighScore;
        _gameData.coins = _coinManager.CoinNumber;
        SaveSystem.SaveData(_gameData);
    }

    private void LoadData()
    {
        _gameData = SaveSystem.LoadData();
        _scoreManager.HighScore = _gameData.highscore;
        _coinManager.CoinNumber = _gameData.coins;
    }
}
