using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Coins : MonoBehaviour
{
    [SerializeField] private float rotationCoin;
    private PickupAnimation pickupAnimation;
    private ControllerQuality _pauseController;

    private void Start()
    {
        _pauseController = GameObject.Find("UI").GetComponent<ControllerQuality>();
        pickupAnimation = GameObject.Find("PickupAnimation").GetComponent<PickupAnimation>();
    }
    void Update()
    {
        if (!_pauseController.isPause)
        {
            transform.Rotate(0, 0, rotationCoin * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<CoinManager>().AddCoinToCollect();
            pickupAnimation.SpawnText();
            Destroy(gameObject);
        }
    }
}
