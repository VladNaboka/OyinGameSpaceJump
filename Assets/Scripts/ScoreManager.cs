using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private TMP_Text _highScoreText;
    public int HighScore { get; set; }
    private GameData _gameData = new GameData();

    private void Start()
    {
        _highScoreText.text = HighScore.ToString();
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "GameScene")
        {
            _scoreText.text = ((int)(_player.position.z / 2)).ToString();
            if (HighScore < (int)(_player.position.z / 2))
            {
                HighScore = (int)(_player.position.z / 2);
                _highScoreText.text = HighScore.ToString();
            }
        }
    }
}
