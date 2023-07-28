using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISoundManager : MonoBehaviour
{
    public AudioSource clickSound;
    public static UISoundManager instance;

    private string nameScene;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void OnClickSound()
    {
        clickSound.Play();
    }

    public void SaveScene()
    {
        nameScene = SceneManager.GetActiveScene().name;
    }
    public string LoadScene()
    {
        return nameScene;
    }
}
