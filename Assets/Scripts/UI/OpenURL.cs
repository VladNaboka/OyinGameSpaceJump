using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public void OpenTelegram()
    {
        UISoundManager.instance.OnClickSound();
        Application.OpenURL("https://web.telegram.org/k/#@oiyndev");
    }
    public void OpenInstagram()
    {
        UISoundManager.instance.OnClickSound();
        Application.OpenURL("https://www.instagram.com/oiyn_dev_studio/");
    }
}
