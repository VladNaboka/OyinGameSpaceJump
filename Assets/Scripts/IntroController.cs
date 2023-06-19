using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroController : MonoBehaviour
{

    private void Awake()
    {
        if (PlayerPrefs.HasKey("_isWatched"))
            if (PlayerPrefs.GetInt("_isWatched") == 1)
                SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
    public void Next()
    {
        PlayerPrefs.SetInt("_isWatched", 1);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }

    public void Reset()
    {
        PlayerPrefs.HasKey("_isWatched");
    }
}
