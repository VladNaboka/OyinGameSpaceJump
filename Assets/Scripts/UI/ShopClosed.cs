using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopClosed : MonoBehaviour
{
    [SerializeField] private GameObject _textBlock;
    [SerializeField] private TextMeshProUGUI _textField;
    [SerializeField] private string[] _texts;

    public void OpenShopTab()
    {
        _textBlock.SetActive(false);
        int randNum = Random.Range(0, _texts.Length);

        _textField.text = _texts[randNum];
        _textBlock.SetActive(true);
    }
}
