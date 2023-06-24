using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public GameObject[] slides;
    private int index = 0;
    private int countSlides;
    void Start()
    {

        countSlides = slides.Length;
        countSlides -= 1;

    }


    void Update()
    {
        index = Mathf.Clamp(index, 0, countSlides);
        if (index == 0)
        {
            slides[0].gameObject.SetActive(true);
        }
    }

    public void Next()
    {
     
        index += 1;
        index = Mathf.Clamp(index, 0, countSlides);
        for (int i = 0; i < slides.Length; i++)
        {
            slides[i].gameObject.SetActive(false);
            slides[index].gameObject.SetActive(true);
        }
    }
    public void Previous()
    {
        index = Mathf.Clamp(index, 0, countSlides);
        index -= 1;

        for (int i = 0; i < slides.Length; i++)
        {
            slides[i].gameObject.SetActive(false);
            slides[index].gameObject.SetActive(true);
        }
    }
}
