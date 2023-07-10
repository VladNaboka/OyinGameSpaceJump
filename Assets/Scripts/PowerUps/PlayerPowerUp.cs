using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPowerUp : MonoBehaviour
{
    [SerializeField] private GameObject _magnetField;
    public SoundManager magnet;
    public void Magnet()
    {
        StartCoroutine(MagnetToPlayer());
    }
    public IEnumerator MagnetToPlayer()
    {
        Debug.Log("Magnet active!");
        magnet.PlayMagnetPickupSound();
        _magnetField.SetActive(true);
        yield return new WaitForSeconds(5);
        _magnetField.SetActive(false);
        Debug.Log("Magnet de-active!");
    } 
}
