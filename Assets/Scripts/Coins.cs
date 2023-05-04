using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coins : MonoBehaviour
{
    [SerializeField] private float rotationCoin;
    void Update()
    {
        transform.Rotate(0, rotationCoin * Time.deltaTime, 0);
    }
    private void OnTriggerEnter(Collider other)
    {
        FindObjectOfType<CoinManager>().AddCoinToCollect();
        Destroy(gameObject);
    }
}
