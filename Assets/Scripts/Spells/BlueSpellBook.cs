using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpellBook : MonoBehaviour
{
    public Image image;
    public Sprite[] blueSpriteArray;
    public Sprite[] redSpriteArray;
    public float speedSlide;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void OpenBlueBook()
    {
        gameObject.SetActive(true);
        StartCoroutine(PlayBlueAnimUIHanlder());
    }

    public void OpenRedBook()
    {
        gameObject.SetActive(true);
        StartCoroutine(PlayRedAnimUIHanlder());
    }

    public void CloseBook()
    {
        StopCoroutine(PlayBlueAnimUIHanlder());
        StopCoroutine(PlayRedAnimUIHanlder());
        gameObject.SetActive(false);
    }

    IEnumerator PlayBlueAnimUIHanlder()
    {
        foreach (Sprite _sprite in blueSpriteArray)
        {
            image.sprite = _sprite;
            yield return new WaitForSeconds(speedSlide);
        }
    }

    IEnumerator PlayRedAnimUIHanlder()
    {
        foreach (Sprite _sprite in redSpriteArray)
        {
            image.sprite = _sprite;
            yield return new WaitForSeconds(speedSlide);
        }
    }
}
