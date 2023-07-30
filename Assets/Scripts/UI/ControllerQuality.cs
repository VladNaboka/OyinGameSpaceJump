using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerQuality : MonoBehaviour
{
    public GameObject[] gameObjects;
    public GameObject pauseMenu;
    public SoundManager sfx;
    public GameObject player;
    public Animator robotAn;
    public Animator curvedAnim;
    public Animator levelAnim;
    public PlayerInput playerInput;
    public bool isPause;

    private IEnumerator _speedCoroutine;

    private void Awake()
    {
        _speedCoroutine = player.GetComponent<PlayerController>().IncreaseSpeed();
    }
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
        UISoundManager.instance.OnClickSound();
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        if (pauseMenu.activeSelf)
        {
            robotAn.StartPlayback();
            curvedAnim.speed = 0;
            levelAnim.speed = 0;
            isPause = true;
            playerInput.enabled = false;
            player.GetComponent<PlayerController>().enabled = false;
            player.GetComponent<GroundCheck>().enabled = false;
            player.GetComponent<PlayerDeath>().enabled = false;
            sfx.MuteSoundOff();
            DOTween.Clear(true);

            for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i].gameObject.SetActive(false);
            }
        }
        if (!pauseMenu.activeSelf)
        {
            robotAn.StopPlayback();
            curvedAnim.speed = 1;
            levelAnim.speed = 1;
            isPause = false;
            playerInput.enabled = true;
            player.GetComponent<PlayerController>().enabled = true;
            player.GetComponent<GroundCheck>().enabled = true;
            player.GetComponent<PlayerDeath>().enabled = true;
            sfx.MuteSoundOn();

            for (int i = 0; i < gameObjects.Length; i++)
            {
                gameObjects[i].gameObject.SetActive(true);
            }
        }
    }
}
