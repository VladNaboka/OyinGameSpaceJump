using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerQuality : MonoBehaviour
{
    public GameObject pauseMenu;
    public SoundManager sfx;
    public GameObject player;
    public Animator robotAn;
    public void SetQualityLevel(int level)
    {
        // Проверить, что уровень находится в допустимом диапазоне
        if (level < 0 || level > QualitySettings.names.Length - 1)
        {
            Debug.LogError("Invalid quality level!");
            return;
        }

        // Установить уровень качества графики
        QualitySettings.SetQualityLevel(level, true);
    }
    
    public void OpenPause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if (pauseMenu.activeSelf)
        {
            robotAn.StartPlayback();
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<GroundCheck>().enabled = false;
            player.GetComponent<PlayerDeath>().enabled = false;
            sfx.MuteSoundOff();
        }
        if (!pauseMenu.activeSelf)
        {
            robotAn.StopPlayback();
            player.GetComponent<PlayerController>().enabled = true;
            player.GetComponent<GroundCheck>().enabled = true;
            player.GetComponent<PlayerDeath>().enabled = true;
            sfx.MuteSoundOn();
        }
    }
}
