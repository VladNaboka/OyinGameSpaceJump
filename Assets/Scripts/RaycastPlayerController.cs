using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastPlayerController : MonoBehaviour
{
    RaycastHit hit;
    public float rayDistance;

    public PlayerController playerController;
    private void Update()
    {
        Debug.DrawRay(transform.position, Vector3.right, Color.yellow);
        Debug.DrawRay(transform.position, Vector3.left, Color.red);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.right), out hit))
        {
            if (playerController.raycastSwiped == 2 && hit.collider.gameObject.name != "")
                Debug.Log("ну еаюрэ рш бпегюкяъ б опюбсч ярнпнмс");
        }
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.left), out hit))
        {
            if (playerController.raycastSwiped == 1 && hit.collider.gameObject.name != "")
                Debug.Log("ну еаюрэ рш бпегюкяъ б кебсч ярнпнмс");
        }
    }
}
