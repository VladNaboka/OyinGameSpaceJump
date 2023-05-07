using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupAnimation : MonoBehaviour
{
    [SerializeField] private GameObject pickupText;
    private Transform spawnPosition;

    private void Awake()
    {
        spawnPosition = GetComponent<Transform>();
    }
    public void SpawnText()
    {
        Debug.Log("spawn");
        Instantiate(pickupText, spawnPosition);
    }
}
