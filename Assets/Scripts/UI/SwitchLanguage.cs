using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Unity.VisualScripting.Icons;

public class SwitchLanguage : MonoBehaviour
{
    public string[] languages;
    public Toggle[] toggles;
    private int currentLanguageIndex;
    private int toggleIndex;

    private void Start()
    {
        //currentLanguageIndex = 0;
        toggleIndex = PlayerPrefs.GetInt("Toggle"); 
        Debug.Log("Toggle index is:" + toggleIndex);
        Debug.Log("Language is:" + PlayerPrefs.GetString("Language"));
        toggles[toggleIndex].isOn = true;
    }
    public void Next() 
    {
        UISoundManager.instance.OnClickSound();
        SwitchLanguageController(1);
    }
    public void Back()
    {
        UISoundManager.instance.OnClickSound();
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
        Debug.Log("Language selected: " + language);
    }

    private void IsOnToggle(int toggle)
    {
        Debug.Log("Toggle index: " + toggle);
        PlayerPrefs.SetInt("Toggle", toggle);
    }
}
