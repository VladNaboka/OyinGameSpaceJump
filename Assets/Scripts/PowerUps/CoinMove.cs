using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMove : MonoBehaviour
{
    [SerializeField] private float _magneticTime;
    [SerializeField] private Transform _playerPos;
    private void Start()
    {
        Debug.Log(_playerPos);
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            //Debug.Log("magnitnulo0");
            //other.transform.position = Vector3.Lerp(transform.position, _playerPos.position, 50);
            other.transform.DOMove(_playerPos.position, _magneticTime);
            //other.gameObject.transform.position = _playerPos.position;
        }

        //Debug.Log("Magnetic power");
        //Debug.Log(_playerPos.position);
        //transform.position = _playerPos.position;
            //Vector3.Lerp(transform.position, _playerPos.position, _magneticSpeed);
        //transform.DOMove(_playerPos.position, _magneticSpeed);
    }
}
