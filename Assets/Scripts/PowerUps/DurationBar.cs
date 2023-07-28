using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DurationBar : MonoBehaviour
{
    [SerializeField] private float _duration;
    private Slider _bar;

    private void Awake()
    {
        _bar = GetComponent<Slider>();
    }
    private void FixedUpdate()
    {
        if(_bar.value > 0)
        {
            _bar.value -= Time.deltaTime;
            //DOTween.To(() => _bar.value, x => _bar.value = x, 0, _duration);
        }
        if(_bar.value <= 0 )
        {
            gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        SetValue();
    }

    private void SetValue()
    {
        _bar.maxValue = _duration;
        _bar.value = _duration;
    }
}
