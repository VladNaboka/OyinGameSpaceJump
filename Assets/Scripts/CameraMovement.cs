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

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }

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
        StartCoroutine(SlideDown());
    }

    private IEnumerator SlideDown()
    {
        _distance += new Vector3(0, -0.3f, 0);
        yield return new WaitForSeconds(1);
        _distance += new Vector3(0, 0.3f, 0);
    }
}
