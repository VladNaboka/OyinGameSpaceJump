using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchLanguage : MonoBehaviour
{
    public string[] languages;
    public Toggle[] toggles;
    private int currentLanguageIndex;
    private int toggleIndex;

    private void Awake()
    {
        currentLanguageIndex = 0;
        toggleIndex = PlayerPrefs.GetInt("Toggle");
        toggles[toggleIndex].isOn = true;
    }

    public void English()
    {
        currentLanguageIndex = 1;
        string selectedLanguage = languages[currentLanguageIndex];
        toggleIndex = 1;

        SwitchToLanguage(selectedLanguage);
        IsOnToggle(toggleIndex);
    }

    public void Russian()
    {
        currentLanguageIndex = 0;
        string selectedLanguage = languages[currentLanguageIndex];
        toggleIndex = 0;

        SwitchToLanguage(selectedLanguage);
        IsOnToggle(toggleIndex);
    }

    public void Kazakh()
    {
        currentLanguageIndex = 2;
        string selectedLanguage = languages[currentLanguageIndex];
        toggleIndex = 2;

        SwitchToLanguage(selectedLanguage);
        IsOnToggle(toggleIndex);
    }

    private void SwitchToLanguage(string language)
    {
        PlayerPrefs.SetString("Language", language);
    }

    private void IsOnToggle(int toggle)
    {
        PlayerPrefs.SetInt("Toggle", toggle);
    }
}