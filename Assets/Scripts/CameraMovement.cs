using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Range(0f, 5f)]
    [SerializeField] private float smooth;
    [SerializeField] private PlayerInput _swipeController;
    [SerializeField] private Vector3 _distance;
    public Transform playerPos;
    private bool _isSliding;

    private void OnEnable()
    {
        _swipeController.Slided += OnSlided;
    }

    private void OnDisable()
    {
        _swipeController.Slided -= OnSlided;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(playerPos.position.x, playerPos.position.y, 0) 
            + _distance, smooth * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, playerPos.position.z + _distance.z);
    }

    private void OnSlided()
    {
        if(_isSliding != true)
            StartCoroutine(SlideDown());
    }

    private IEnumerator SlideDown()
    {
        _isSliding = true;
        _distance += new Vector3(0, -0.2f, 0);
        yield return new WaitForSeconds(1);
        _isSliding = false;
        _distance += new Vector3(0, 0.2f, 0);
    }
}
