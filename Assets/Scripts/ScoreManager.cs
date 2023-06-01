using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private PlayerDeath _playerDeath;
    [SerializeField] private Transform _player;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TMP_Text _highScoreText;
    private int _highScore;
    private GameData _gameData = new GameData();

    private void OnEnable()
    {
        _playerDeath.OnPlayerDied += OnDied;
    }

    private void OnDisable()
    {
        _playerDeath.OnPlayerDied -= OnDied;
    }

    private void Start()
    {
        LoadData();
    }

    private void Update()
    {
        _scoreText.text = ((int)(_player.position.z / 2)).ToString();
    }

    private void LoadData()
    {
        _gameData = SaveSystem.LoadData();
        _highScore = _gameData.highscore;
        _highScoreText.text = _highScore.ToString();
    }

    private void OnDied()
    {
        _highScore = _gameData.highscore;
        if (_highScore < (int)(_player.position.z / 2))
        {
            _highScore = (int)(_player.position.z / 2);
            _gameData.highscore = _highScore;
            SaveSystem.SaveData(_gameData);
        }
        _highScoreText.text = _highScore.ToString();
    }
}
