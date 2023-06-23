using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLanguage : MonoBehaviour
{
    public GameObject[] slides;
    private int index = 0;
    private int countSlides;
    void Start()
    {
        countSlides = slides.Length;
    }

    void Update()
    {
        if (index >= countSlides)
        {
            index = countSlides;
        }
        if (index < 0)
        {
            index = 0;
        }
        if (index == 0)
        {
            slides[0].gameObject.SetActive(true);
        }

    }

    public void Next()
    {
        index = Mathf.Clamp(index, 0, slides.Length);
        index += 1;

        for (int i = 0; i < slides.Length; i++)
        {
            slides[i].gameObject.SetActive(false);
            slides[index].gameObject.SetActive(true);
        }
    }
    public void Previous()
    {
        index = Mathf.Clamp(index, 0, slides.Length);
        index -= 1;

        for (int i = 0; i < slides.Length; i++)
        {
            slides[i].gameObject.SetActive(false);
            slides[index].gameObject.SetActive(true);
        }
    }
}
