using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Range(0f, 5f)]
    [SerializeField] private float smooth;
    public Transform playerPos;
    public Vector3 distance;

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, new Vector3(playerPos.position.x, playerPos.position.y, 0) 
            + distance, smooth * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, playerPos.position.z + distance.z);
    }
}
