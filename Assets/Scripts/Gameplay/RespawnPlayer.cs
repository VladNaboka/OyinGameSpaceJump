using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System;

public class RespawnPlayer : MonoBehaviour
{
    [SerializeField] private PlayerController playerController;
    [SerializeField] private ScoreManager _scoreManager;
    [SerializeField] private CoinManager _coinManager;

    [SerializeField] private float speedPlayer;
    public int score;
    public int coinScore;

    public static RespawnPlayer Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if (SceneManager.GetActiveScene().name == "GameScene")
            DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (speedPlayer > 6.7f)
            playerController._playerSpeed = speedPlayer;
        _scoreManager.score = score.ToString();
        _coinManager.NumberCoin = coinScore;
    }

    public void Respawn()
    {
        PlayerSpeeded();
        score = Convert.ToInt32(_scoreManager.score);
        coinScore = _coinManager.NumberCoin;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Update()
    {
        PlayerSpeeded();
    }

    private float PlayerSpeeded()
    {
        if (playerController._playerSpeed > 6.7f)
            return speedPlayer = playerController._playerSpeed;
        return 0;
    }
}
