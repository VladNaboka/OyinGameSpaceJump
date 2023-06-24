using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager Instance;
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

    public void Ru()
    {
        string language = "Ru";
        PlayerPrefs.SetString("Language", language);
    }

    public void Eng()
    {
        string language = "Eng";
        PlayerPrefs.SetString("Language", language);
    }

    public void Kz()
    {
        string language = "Kz";
        PlayerPrefs.SetString("Language", language);
    }
}
