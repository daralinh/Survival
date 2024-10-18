using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AboutMe : MonoBehaviour
{
    public Image myInforImage;

    void Start()
    {
        myInforImage.gameObject.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetMouseButton(1))
        {
            Close();
        }
    }

    public void Open()
    {
        myInforImage.gameObject.SetActive(true);
    }

    public void Close()
    {
        myInforImage.gameObject.SetActive(false);
    }

    public void OpenFacebook()
    {
        Application.OpenURL("https://www.facebook.com/Linkdara.vn/");
    }

    public void OpenGithub()
    {
        Application.OpenURL("https://github.com/daralinh");
    }

    public void OpenMail()
    {
        Application.OpenURL($"mailto:Khanhtran2002nd@gmail.com");
    }
}
