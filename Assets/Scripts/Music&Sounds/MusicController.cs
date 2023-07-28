using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    [SerializeField] private Sprite spriteMusicOFF;
    [SerializeField] private Sprite spriteMusicON;
    bool activ = true;
    private void Start()
    {
        if (AudioListener.volume == 1f)
        {
            activ = true;
            gameObject.GetComponent<Image>().sprite = spriteMusicON;

        }
        else if (AudioListener.volume == 0f)
        {
            activ = false;
            gameObject.GetComponent<Image>().sprite = spriteMusicOFF;

        }
    }
    public void MusicOut()
    {
        //AudioListener audioListener = FindObjectOfType<AudioListener>();
        activ = !activ;
        if (activ)
        {
            AudioListener.volume = 1f;
            //audioListener.enabled = true;
            gameObject.GetComponent<Image>().sprite = spriteMusicON;
        }
        else
        {
            AudioListener.volume = 0f;
            //audioListener.enabled = false;
            gameObject.GetComponent<Image>().sprite = spriteMusicOFF;
        }
    }
}
