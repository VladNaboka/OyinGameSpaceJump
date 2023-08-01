using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RespawnScreen : MonoBehaviour
{
    [SerializeField] private Slider _waitingBar;
    [SerializeField] private GameObject _gameOverScreen;
    [SerializeField] private float _incrementValue;

    private void Update()
    {
        _waitingBar.value -= _incrementValue * Time.deltaTime;
        if(_waitingBar.value <= 0)
        {
            CloseRespawnMenu();
        }
    }
    public void CloseRespawnMenu()
    {
        _gameOverScreen.SetActive(true);
        gameObject.SetActive(false);
    }
}
