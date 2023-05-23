using UnityEngine;

public class FpsController : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
