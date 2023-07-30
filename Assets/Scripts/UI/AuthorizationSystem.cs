using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AuthorizationSystem : MonoBehaviour
{
    [SerializeField] private InputField _inputField;
    [SerializeField] private Text _usernameText;
    private string _inputName;

    public static string _playerName;
    public static bool _isAuth;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("Username"))
        {
            _playerName = PlayerPrefs.GetString("Username");
            _isAuth = true;
        }
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            _usernameText.text = _playerName;
        }
    }

    public void InputName()
    {   
        _inputName = _inputField.text;
    }
    public void Apply()
    {
        if (_inputName == null)
        {
            Debug.Log("¬веди им€");
            return;
        }
        _playerName = _inputName;
        Debug.Log(_playerName);
        PlayerPrefs.SetString("Username", _playerName);
        
    }
}
