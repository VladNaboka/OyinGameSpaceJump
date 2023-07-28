using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPlayerController : MonoBehaviour
{
    [SerializeField] private float rayDistance;
    [Range(-1f, 1f)] [SerializeField] private float _yDistance = -0.1f;
    [Range(-1f, 1f)] [SerializeField] private float _zDistance;
    private RaycastHit hit;

    private PlayerController _playerController;
    private void Awake()
    {
        _playerController = GetComponent<PlayerController>();
    }
    private void Update()
    {
        Debug.DrawRay(transform.position + new Vector3(0, _yDistance, _zDistance), Vector3.right * rayDistance, Color.yellow);
        Debug.DrawRay(transform.position + new Vector3(0, _yDistance, _zDistance), Vector3.left * rayDistance, Color.red);

        if (Physics.Raycast(transform.position + new Vector3(0, _yDistance, _zDistance), 
            transform.TransformDirection(Vector3.right * rayDistance), out hit))
        {
            if (hit.collider.gameObject.name == "HighBoxObstacle")
            {
                _playerController._rightObstacle = true;
            }
        }
        else
        {
            _playerController._rightObstacle = false;
        }


        if (Physics.Raycast(transform.position + new Vector3(0, _yDistance, _zDistance), 
            transform.TransformDirection(Vector3.left * rayDistance), out hit))
        {
            if (hit.collider.gameObject.name == "HighBoxObstacle")
            {
                _playerController._leftObstacle = true;
            }
        }
        else
        {
            _playerController._leftObstacle = false;
        }
    }
}
