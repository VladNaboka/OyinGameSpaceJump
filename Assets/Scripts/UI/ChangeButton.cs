using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeButton : MonoBehaviour
{
    public Sprite spriteMusicOFF;
    public Sprite spriteMusicON;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            spriteRenderer.sprite = spriteMusicON;
        }
    }
    public void ChangeButtonImage()
    {
        if (spriteRenderer == spriteMusicON)
        {
            gameObject.GetComponent<Image>().sprite = spriteMusicOFF;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = spriteMusicON;
        }
    }
}
