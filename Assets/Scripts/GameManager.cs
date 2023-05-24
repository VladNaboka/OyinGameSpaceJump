using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _gameOver;
    [SerializeField] private Animator _anim;

    private string sceneToLoad;

    public void GameOverScreen()
    {
        _gameOver.SetActive(true);
    }
    public void FadeOut(string sceneName)
    {
        sceneToLoad = sceneName;
        _anim.SetTrigger("FadeOut");
    }
    public void LoadScene()
    {
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
        Application.Quit();
    }
}
