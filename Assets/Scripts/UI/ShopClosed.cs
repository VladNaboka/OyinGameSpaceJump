using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopClosed : MonoBehaviour
{
    [SerializeField] private GameObject _shopTab;
    [SerializeField] private Animator _animator;
    private bool _isUnActive;

    public void OpenShopTab()
    {
        if(_isUnActive) _shopTab.SetActive(false);
        _shopTab.SetActive(true);
        _isUnActive = false;
        
    }
    public void CloseShopTab()
    {
        _isUnActive = true;
        _animator.SetTrigger("Close");
    }
}
