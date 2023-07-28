using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateRobot : MonoBehaviour
{
    [SerializeField] private Vector3 _rotation;
    [SerializeField] private float _speedRotation;


    private Touch touch;

    private Vector2 touchPosition;

    private Quaternion rotationY;

    public float rotateTouchSpeed = 10f;

    void Update()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if(touch.phase == TouchPhase.Moved )
            {
                rotationY = Quaternion.Euler(0f , -touch.deltaPosition.x * rotateTouchSpeed, 0f );
                transform.rotation = rotationY * transform.rotation;
            }
        }

        transform.Rotate(_rotation * _speedRotation * Time.deltaTime);
    }
}
