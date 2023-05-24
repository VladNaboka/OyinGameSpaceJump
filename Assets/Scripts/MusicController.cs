using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    [SerializeField] private Sprite spriteMusicOFF;
    [SerializeField] private Sprite spriteMusicON;
    bool activ = true;
    public void MusicOut()
    {
        AudioListener audioListener = FindObjectOfType<AudioListener>();
        activ = !activ;
        if (activ)
        {
            audioListener.enabled = true;
            gameObject.GetComponent<Image>().sprite = spriteMusicON;
        }
        else
        {
            audioListener.enabled = false;
            gameObject.GetComponent<Image>().sprite = spriteMusicOFF;
        }
    }
}
