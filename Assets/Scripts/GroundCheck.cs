using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private float rayDistance;
    [NonSerialized] public bool groundCheck;

    private void Update()
    {
        groundCheck = Physics.Raycast(transform.position, Vector3.down, rayDistance, LayerMask.GetMask("Ground"));
        Debug.DrawRay(transform.position, new Vector3(0, rayDistance * -1f, 0), Color.green);
    }
}
