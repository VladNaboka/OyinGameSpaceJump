using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MenuCoinsAndMaxRec : MonoBehaviour
{
    private GameData _gameData = new GameData();
    [SerializeField] private TextMeshProUGUI _scoreManagerText;
    [SerializeField] private TextMeshProUGUI _coinManagerText;

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
