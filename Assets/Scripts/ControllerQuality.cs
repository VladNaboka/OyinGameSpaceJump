using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerQuality : MonoBehaviour
{
    public GameObject pauseMenu;
    public SoundManager sfx;
    public void SetQualityLevel(int level)
    {
        // ���������, ��� ������� ��������� � ���������� ���������
        if (level < 0 || level > QualitySettings.names.Length - 1)
        {
            Debug.LogError("Invalid quality level!");
            return;
        }

        // ���������� ������� �������� �������
        QualitySettings.SetQualityLevel(level, true);
    }
    
    public void OpenPause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if (pauseMenu.activeSelf)
        {
            Time.timeScale = 0f;
            sfx.MuteSoundOff();
        }
        if (!pauseMenu.activeSelf)
        {
            Time.timeScale = 1f;
            sfx.MuteSoundOn();
        }
    }
}
