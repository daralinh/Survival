using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicBook : MonoBehaviour
{
    private RectTransform rectTransform;

    public Image image;
    public Sprite[] blueSpriteArray;
    public Sprite[] redSpriteArray;
    public float speedSlide;
    public Button leftButton;
    public Button rightButton;

    public List<AUpgrade> upgradeList;
    public List<AUpgrade> specialUpgradeList;

    private bool isBlue;

    private void Awake()
    {
        isBlue = true;
        rectTransform = image.GetComponent<RectTransform>();
        HideButton();
        gameObject.SetActive(false);
    }

    public void OpenBlueBook()
    {
        isBlue = true;
        gameObject.SetActive(true);
        StartCoroutine(PlayOpenBlueAnimUIHanlder());
    }

    public void OpenRedBook()
    {
        isBlue = false;
        gameObject.SetActive(true);
        StartCoroutine(PlayOpenRedAnimUIHanlder());
    }

    private void ShowButton()
    {
        if (isBlue)
        {
            
        }
        else
        {

        }

        leftButton.gameObject.SetActive(true);
        rightButton.gameObject.SetActive(true);
    }

    private void HideButton()
    {
        leftButton.gameObject.SetActive(false);
        rightButton.gameObject.SetActive(false);
    }

    public void CloseBook()
    {
        if (isBlue)
        {
            StartCoroutine(PlayCloseBlueAnimUIHanlder());
        }
        else
        {
            StartCoroutine(PlayCloseRedAnimUIHanlder());
        }
    }

    IEnumerator PlayOpenBlueAnimUIHanlder()
    {
        foreach (Sprite _sprite in blueSpriteArray)
        {
            SetPivotBySprite(_sprite);
            image.sprite = _sprite;
            yield return new WaitForSeconds(speedSlide);
        }

        ShowButton();
    }

    IEnumerator PlayOpenRedAnimUIHanlder()
    {
        foreach (Sprite _sprite in redSpriteArray)
        {
            SetPivotBySprite(_sprite);
            image.sprite = _sprite;
            yield return new WaitForSeconds(speedSlide);
        }

        ShowButton();
    }

    IEnumerator PlayCloseBlueAnimUIHanlder()
    {
        for (int i = blueSpriteArray.Length - 1; i >= 0; i--)
        {
            SetPivotBySprite(blueSpriteArray[i]);
            image.sprite = blueSpriteArray[i];
            yield return new WaitForSeconds(speedSlide);
        }

        HideButton();
        gameObject.SetActive(false);
    }

    IEnumerator PlayCloseRedAnimUIHanlder()
    {
        for (int i = redSpriteArray.Length - 1; i >= 0; i--)
        {
            SetPivotBySprite(redSpriteArray[i]);
            image.sprite = redSpriteArray[i];
            yield return new WaitForSeconds(speedSlide);
        }

        HideButton();
        gameObject.SetActive(false);
    }

    private void SetPivotBySprite(Sprite _sprite)
    {
        rectTransform.pivot = new Vector2(_sprite.pivot.x / _sprite.rect.width, _sprite.pivot.y / _sprite.rect.height);
    }
}
