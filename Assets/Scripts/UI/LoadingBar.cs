using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private GameObject _music;
    [SerializeField] private GameObject _loadingScreen;
    [SerializeField] private float _loadingAmount;
    private Slider _slider;


    private void Awake()
    {
        _slider = GetComponent<Slider>();

    }
    private void Update()
    {
        _slider.value += _loadingAmount * Time.deltaTime;
        if(_slider.value > 60)
        {
            _loadingAmount = 30;
        }
        if (_slider.value >= 100)
        {
            _loadingScreen.SetActive(false);
            _music.SetActive(true);
        }
    }
}
