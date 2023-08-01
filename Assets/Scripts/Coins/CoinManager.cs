using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private PlayerStatsScriptableObject _playerStatsScriptableObject;
    [SerializeField] TextMeshProUGUI _textMP;
    [SerializeField] Text _coinRunText;
    public int CoinNumber;
    public int CoinRun { get; set; }
    private GameData _gameData = new GameData();

    public SoundManager sfx;

    private void Start()
    {
        _textMP.text = CoinNumber.ToString();
    }

    public void AddCoinToCollect()
    {
        CoinNumber++;
        CoinRun++;
        sfx.PlayCoinPickupSound();
        _textMP.text = CoinNumber.ToString();
        _coinRunText.text = CoinRun.ToString();
    }
}