using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivityManager : MonoBehaviour
{
    public GameObject[] targetObjects;
    public GameObject[] offObjects;

    public void EnableObject()
    {
        foreach (GameObject obj in targetObjects)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in offObjects)
        {
            obj.SetActive(false);
        }
    }

    public void DisableObject()
    {
        foreach (GameObject obj in targetObjects)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in offObjects)
        {
            obj.SetActive(true);
        }
    }
}

