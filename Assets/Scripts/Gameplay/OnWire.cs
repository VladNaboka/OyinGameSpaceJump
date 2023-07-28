using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnWire : MonoBehaviour
{
    [SerializeField] private GameObject particleWire;
    private Transform player;
    private ControllerQuality _pauseController;
    private void Start()
    {
        player = GameObject.Find("Player").transform;
        _pauseController = GameObject.Find("UI").GetComponent<ControllerQuality>();
    }
    void Update()
    {
        if (_pauseController.isPause)
        {
            particleWire.SetActive(false);
        }
        if (Vector3.Distance(gameObject.transform.position, player.position) < 15 && !_pauseController.isPause)
        {
            particleWire.SetActive(true);
        }
    }
}
