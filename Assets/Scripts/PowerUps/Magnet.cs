using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    private PlayerPowerUp _pwUp;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Magnet picked up!");
            _pwUp = other.GetComponent<PlayerPowerUp>();
            _pwUp.Magnet();
            Destroy(gameObject);
        }
    }
}
