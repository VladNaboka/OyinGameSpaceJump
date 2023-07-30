using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class Graphic : MonoBehaviour
{
    public static Graphic Instance;
    [SerializeField] private GameObject volume;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Graphic") == 1)
        {
            volume.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("Graphic") == 0)
        {
            volume.SetActive(false);
        }
    }
}
