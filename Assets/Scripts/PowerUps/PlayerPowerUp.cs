using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    [SerializeField] private GameObject _magnetField;
    [SerializeField] private GameObject _magnetBar;
    [SerializeField] private float _magnetDuration;
    public SoundManager magnet;
    public bool _isActive;
    public void Magnet()
    {
        if (_isActive)
        {
            StopCoroutine(MagnetToPlayer());
        }
        StartCoroutine(MagnetToPlayer());
    }
    public IEnumerator MagnetToPlayer()
    {
        _isActive = true;
        _magnetBar.SetActive(false);
        Debug.Log("Magnet active!");
        magnet.PlayMagnetPickupSound();
        _magnetBar.SetActive(true);
        _magnetField.SetActive(true);
        yield return new WaitForSeconds(_magnetDuration);
        _isActive = false;
        _magnetField.SetActive(false);
        Debug.Log("Magnet de-active!");
    } 
}
