using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWire : MonoBehaviour
{
    [SerializeField] private GameObject particleWire;
    public Transform player;
    private void Start()
    {
        player = GameObject.Find("Player").transform;
    }
    void Update()
    {
        if (Vector3.Distance(gameObject.transform.position, player.position) < 15)
        {
            particleWire.SetActive(true);
        }
    }
}
