using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomSpawn : MonoBehaviour
{
    [SerializeField]private GameObject[] _objects;
    private Transform _position;
    private int _randomNum;
    void Start()
    {
        _randomNum = Random.Range(0, _objects.Length);
        _position = GetComponent<Transform>();
        Instantiate(_objects[_randomNum], _position);
    }
    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.R))
    //    {
    //        SceneManager.LoadScene(2);
    //    }
    //}
}
