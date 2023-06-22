using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenURL : MonoBehaviour
{
    public void OpenTelegram()
    {
        Application.OpenURL("https://web.telegram.org/k/#@oiyndev");
    }
    public void OpenInstagram()
    {
        Application.OpenURL("https://www.instagram.com/oiyn_dev_studio/");
    }
}
