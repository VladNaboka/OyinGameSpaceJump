using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializeManager : MonoBehaviour
{
    private GameData _gameData = new GameData();

    private void Awake()
    {
        SaveSystem.CreateDataFile(_gameData);
    }
}
