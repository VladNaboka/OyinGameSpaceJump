using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Этот скрипт используется для взаимодействия с UI элементами
public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOver;

    public void GameOverScreen()
    {
        gameOver.SetActive(true);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void CloseApp()
    {
        Application.Quit();
    }
}
