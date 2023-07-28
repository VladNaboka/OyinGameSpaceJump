using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _textMP;
    public int NumberCoin { get; set;}
    private GameData _gameData = new GameData();

    public SoundManager sfx;

    private void Start()
    {
        NumberCoin = RespawnPlayer.Instance.coinScore;
        _textMP.text = NumberCoin.ToString();
    }

    public void AddCoinToCollect()
    {
        NumberCoin++;
        sfx.PlayCoinPickupSound();
        _textMP.text = NumberCoin.ToString();
    }
}