using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoinManager : MonoBehaviour
{
    public int NumberCoin = 0;
    [SerializeField] TextMeshProUGUI _textMP;
    private GameData _gameData = new GameData();

    public SoundManager sfx;
    private void Start()
    {
        LoadData();
        _textMP.text = NumberCoin.ToString();
    }
    public void AddCoinToCollect()
    {
        NumberCoin++;
        sfx.PlayCoinPickupSound();
        _textMP.text = NumberCoin.ToString();
        SaveData();
    }

    //����� �������� �� JSON
    public void SaveData()
    {
        _gameData.coins = NumberCoin;
        SaveSystem.SaveData(_gameData);
    }
    public void LoadData()
    {
        _gameData = SaveSystem.LoadData();
        NumberCoin = _gameData.coins;
    }
    /*public void SpendMoney(int value)
    {
        NumberCoin -= value;
        _textMP.text = NumberCoin.ToString();
    }
    */
}
