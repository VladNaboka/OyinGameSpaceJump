﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextLanguage : MonoBehaviour
{
    public string language;
    TextMeshProUGUI text;

    public string textRu;
    public string textEng;
    public string textKz;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        language = PlayerPrefs.GetString("Language");

        if(language == "" || language == "Eng")
        {
            text.text = textEng;
        }
        else if(language == "Ru")
        {
            text.text = textRu;
        }
        else if (language == "Kz")
        {
            text.text = textKz;
        }
    }
}
