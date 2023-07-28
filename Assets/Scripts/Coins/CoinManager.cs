using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    [SerializeField] private PlayerStatsScriptableObject _playerStatsScriptableObject;
    [SerializeField] TextMeshProUGUI _textMP;
    public int CoinNumber { get; set;}
    private GameData _gameData = new GameData();

    public SoundManager sfx;

    private void Start()
    {
        _textMP.text = CoinNumber.ToString();
    }

    public void AddCoinToCollect()
    {
        CoinNumber++;
        sfx.PlayCoinPickupSound();
        _textMP.text = CoinNumber.ToString();
    }
}