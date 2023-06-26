using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchController : MonoBehaviour
{
    public GameObject[] slides;
    public Button nextButton;
    public Button prevButton;

    private int currentSlideIndex = 0;

    private void Start()
    {
        ShowSlide(currentSlideIndex);
        UpdateButtonStates();
    }

    public void NextSlide()
    {
        currentSlideIndex++;
        ShowSlide(currentSlideIndex);
        UpdateButtonStates();
    }

    public void PreviousSlide()
    {
        currentSlideIndex--;
        ShowSlide(currentSlideIndex);
        UpdateButtonStates();
    }

    private void ShowSlide(int index)
    {
        for (int i = 0; i < slides.Length; i++)
        {
            slides[i].SetActive(i == index);
        }
    }

    private void UpdateButtonStates()
    {
        nextButton.interactable = currentSlideIndex < slides.Length - 1;
        prevButton.interactable = currentSlideIndex > 0;
    }
}
