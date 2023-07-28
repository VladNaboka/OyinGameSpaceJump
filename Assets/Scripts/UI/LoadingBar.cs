using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private float _loadingAmount;
    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }
    private void Update()
    {
        _slider.value += _loadingAmount;
        if(_slider.value > 60)
        {
            _loadingAmount = 5;
        }
        if (_slider.value >= 100)
        {
            _gameManager.FadeOutLoading("MainMenu");
        }
    }
}
