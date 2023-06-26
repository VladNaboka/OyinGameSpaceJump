using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicDontDestroy : MonoBehaviour
{
    public static MusicDontDestroy Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            transform.parent = null;
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
