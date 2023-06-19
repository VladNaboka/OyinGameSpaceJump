using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    public Transform targetObject;

    void Update()
    {
        Vector3 newPosition = transform.position;

        newPosition.x = targetObject.position.x;
        newPosition.z = targetObject.position.z;

        transform.position = newPosition;
    }
}

