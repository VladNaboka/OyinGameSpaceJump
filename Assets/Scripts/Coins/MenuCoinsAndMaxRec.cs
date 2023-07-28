using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MenuCoinsAndMaxRec : MonoBehaviour
{
    private GameData _gameData = new GameData();
    [SerializeField] private Text _scoreManagerText;
    [SerializeField] private Text _coinManagerText;

    private void Awake()
    {
        Load();
    }
    private void Load()
    {
        _gameData = SaveSystem.LoadData();
        _scoreManagerText.text = _gameData.highscore.ToString();
        _coinManagerText.text = _gameData.coins.ToString();
        Debug.Log(_gameData.highscore.ToString());
    }
}
