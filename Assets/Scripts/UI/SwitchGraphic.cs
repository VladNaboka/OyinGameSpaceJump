using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Rendering;

public class SwitchGraphic : MonoBehaviour
{
    [SerializeField] private RectTransform UIHandleRectTransform;
    public static GameObject postProcessVolume;
    [SerializeField] Color backgroundDisabledColor;
    [SerializeField] Color handleDisabledColor;
    private int toggleState;

    private Image backgroundImage, handleImage;

    private Color backgroundDefaultColor, handleDefaultColor;

    private Toggle toggle;

    Vector2 handlePosition;

    private void Awake()
    {
        postProcessVolume = GameObject.FindGameObjectWithTag("Volume");

        toggle = GetComponent<Toggle>();

        handlePosition = UIHandleRectTransform.anchoredPosition;
        backgroundImage = UIHandleRectTransform.parent.GetComponent<Image>();
        handleImage = UIHandleRectTransform.GetComponent<Image>();

        backgroundDefaultColor = backgroundImage.color;
        handleDefaultColor = handleImage.color;

        toggle.onValueChanged.AddListener(OnSwitch);

        if (toggle.isOn)
        {
            OnSwitch(true);
        }

        if (PlayerPrefs.GetInt("Graphic") == 1)
        {
            postProcessVolume.SetActive(true);
            toggle.isOn = true;
        }
        else if(PlayerPrefs.GetInt("Graphic") == 0)
        {
            postProcessVolume.SetActive(false);
            toggle.isOn = false;
        }
    }

    public void OnSwitch(bool on)
    {
        UIHandleRectTransform.DOAnchorPos(on ? handlePosition * -1 : handlePosition, .4f).SetEase(Ease.InOutBack);
        backgroundImage.DOColor(!on ? backgroundDisabledColor : backgroundDefaultColor, .6f);
        handleImage.DOColor(!on ? handleDisabledColor : handleDefaultColor, .4f);
    }

    private void OnDestroy()
    {
        toggle.onValueChanged.RemoveListener(OnSwitch);
    }

    public void IsOnToggle(bool toggleState)
    {
        postProcessVolume.gameObject.SetActive(toggleState);
        PlayerPrefs.SetInt("Graphic", toggleState ? 1 : 0);
    }
}
