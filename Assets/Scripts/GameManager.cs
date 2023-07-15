using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private GameObject pauseSprite;
    [SerializeField] private Animator _anim;

    private string sceneToLoad;

    public void GameOverScreen()
    {
        pauseSprite.SetActive(false);
        _gameOver.SetActive(true);
    }
    public void FadeOut(string sceneName)
    {
        UISoundManager.instance.OnClickSound();
        sceneToLoad = sceneName;
        _anim.SetTrigger("FadeOut");
    }
    public void LoadScene()
    {
        Time.timeScale = 1f;
        DOTween.Clear(true);
        SceneManager.LoadScene(sceneToLoad);
    }
    //public void MainMenu()
    //{
    //    SceneManager.LoadScene("MainMenu");
    //}
    //public void StartGame()
    //{
    //    SceneManager.LoadScene("GameScene");
    //}
    public void CloseApp()
    {
        UISoundManager.instance.OnClickSound();
        Application.Quit();
    }

    public void BackScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    public void LoadIntro(string sceneName)
    {
        sceneToLoad = sceneName;
        _anim.SetTrigger("FadeOut");

    }
}

