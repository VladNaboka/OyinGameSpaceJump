using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLanguage : MonoBehaviour
{
    public string[] languages;
    private int currentLanguageIndex;

    private void Start()
    {
        currentLanguageIndex = 0;
    }

    public void Next()
    {
        SwitchLanguageController(1);
    }
    public void Back()
    {
        SwitchLanguageController(-1);
    }

    private void SwitchLanguageController(int direction)
    {
        currentLanguageIndex += direction;

        if (currentLanguageIndex < 0)
        {
            currentLanguageIndex = languages.Length - 1;
        }
        else if (currentLanguageIndex >= languages.Length)
        {
            currentLanguageIndex = 0;
        }

        string selectedLanguage = languages[currentLanguageIndex];

        SwitchToLanguage(selectedLanguage);
    }

    private void SwitchToLanguage(string language)
    {
        PlayerPrefs.SetString("Language", language);
    }
}
