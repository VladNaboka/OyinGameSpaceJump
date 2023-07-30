using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

public class RespawnPlayer : MonoBehaviour
{
    [SerializeField] private PlayerStatsScriptableObject _playerStatsScriptableObject;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private CoinManager _coinManager;

    private void Awake()
    {
        if(_playerStatsScriptableObject.isRestarted)
        {
            playerController._playerSpeed = _playerStatsScriptableObject.speedPlayer;
            _scoreManager.RestartedScore = _playerStatsScriptableObject.restartedScore;
            _coinManager.CoinNumber = _playerStatsScriptableObject.coinScore;
            //_playerStatsScriptableObject.SetDefaultValues();
            _playerStatsScriptableObject.isRestarted = false;
        }
    }

    public void Respawn()
    {
        Debug.Log("Respawn");
        PlayerSpeeded();
        _playerStatsScriptableObject.isRestarted = true;
        _playerStatsScriptableObject.restartedScore = Convert.ToInt32(_scoreManager.score);
        _playerStatsScriptableObject.coinScore = _coinManager.CoinNumber;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        PlayerSpeeded();
    }

    private float PlayerSpeeded()
    {
        if (playerController._playerSpeed > 6.7f)
            return _playerStatsScriptableObject.speedPlayer = playerController._playerSpeed;
        return 0;
    }
}
